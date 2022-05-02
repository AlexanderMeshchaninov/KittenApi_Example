using System;
using System.Threading.Tasks;
using AuthApi.UserRequest;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthApiFluentValidation.Models;
using AuthApiFluentValidation.Services;
using AuthBusinessLayer;
using AuthBusinessLayer.Requests;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IOperationService _authValidator;
        private readonly IClientService _clientService;

        public AuthController(
            ILogger<AuthController> logger,
            IMapper mapper,
            IOperationService authValidator,
            IClientService clientService)
        {
            _logger = logger;
            _mapper = mapper;
            _authValidator = authValidator;
            _clientService = clientService;
        }

        [AllowAnonymous]
        [HttpPost("user_registration")]
        public async Task<IActionResult> UserRegistrationAsync(string userName, string email, string password)
        {
            _logger.LogInformation($"User registration process start: {DateTimeOffset.Now}");

            try
            {
                var request = new UserRequestDto { UserName = userName, Email = email, Password = password };

                var validationRequest = _mapper.Map<UserRequestValidation>(request);
                var result = await _authValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToBusinessLayer = _mapper.Map<RequestFromAuthApiDto>(request);
                var toRepo = _clientService.RegisterUserRequestToRepositoryAsync(requestToBusinessLayer);
                if (toRepo.Exception is not null)
                {
                    return BadRequest("User registration fail");
                }

                _logger.LogInformation($"User registration process stops with success: {DateTimeOffset.Now}");

                return Ok("User has been registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost ("user_login")]
        public async Task<IActionResult> UserLogin(string email, string password)
        {
            _logger.LogInformation($"User login process start: {DateTimeOffset.Now}");

            try
            {
                var request = new UserRequestDto { UserName = "Foo", Email = email, Password = password };

                var validationRequest = _mapper.Map<UserRequestValidation>(request);
                var result = await _authValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToClientService = _mapper.Map<RequestFromAuthApiDto>(request);
                var tokenResponse = await _clientService.AuthenticationRequestAsync(requestToClientService);
                if (string.IsNullOrWhiteSpace(tokenResponse?.Token))
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                await Task.Run(() => SetTokenCookieAsync(tokenResponse.RefreshToken));

                _logger.LogInformation($"User login process stops with success: {DateTimeOffset.Now}");

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            _logger.LogInformation($"Refresh-token process start: {DateTimeOffset.Now}");

            try
            {
                var oldRefreshToken = Request.Cookies["refreshToken"];
                var newRefreshToken = await _clientService.RefreshTokenResponseAsync(oldRefreshToken);
                if (string.IsNullOrWhiteSpace(newRefreshToken))
                {
                    return Unauthorized(new { message = "Invalid Token" });
                }
                await Task.Run(() => SetTokenCookieAsync(newRefreshToken));

                _logger.LogInformation($"Refresh-token process stops with success: {DateTimeOffset.Now}");

                return Ok(newRefreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        private void SetTokenCookieAsync(string token)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,

                Expires = DateTime.UtcNow.AddDays(7),
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
