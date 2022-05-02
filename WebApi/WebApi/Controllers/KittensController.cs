using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebApi.Requests;
using WebApiBusinessLayer;
using WebApiBusinessLayer.Requests;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/kittens")]
    [ApiController]
    public sealed class KittensController : ControllerBase
    {
        private readonly ILogger<KittensController> _logger;
        private readonly IMapper _mapper;
        private readonly IOperationKittenService _kittenValidator;
        private readonly IKittenService _kittenService;

        public KittensController(
            IMapper mapper,
            ILogger<KittensController> logger,
            IOperationKittenService kittenValidator,
            IKittenService kittenService)
        {
            _mapper = mapper;
            _logger = logger;
            _kittenValidator = kittenValidator;
            _kittenService = kittenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterKittenInfoAsync(
            string nickName, 
            int weigth, 
            string color, 
            string feedName,
            bool hasCertificate)
        {
            _logger.LogInformation($"Kitten registration process start: {DateTimeOffset.Now}");

            try
            {
                var request = new KittenRequestDto() { NickName = nickName, Weight = weigth, Color = color, FeedName = feedName, HasCertificate = hasCertificate, Id = 111, Page = 111, Size = 111};
                
                var validationRequest = _mapper.Map<KittenRequestValidation>(request);
                var result = await _kittenValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestKittensFromWebApiDto>(request);
                var res = await _kittenService.RegisterKittenRequestToRepositoryAsync(requestToRepository);
                if (res.Exception is not null)
                {
                    return BadRequest("Kitten doesn't register");
                }

                _logger.LogInformation($"Kitten registration process stops with success: {DateTimeOffset.Now}");

                return Ok("Kitten has been registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpGet("readbyparameters")]
        public async Task<IActionResult> ReadKittensByParametersAsync(
            string kittenNickName, 
            int page, 
            int size)
        {
            _logger.LogInformation($"Kitten read process start: {DateTimeOffset.Now}");

            try
            {
                var request = new KittenRequestDto() { NickName = kittenNickName, Page = page, Size = size, Color = "Foo", FeedName = "Foo", HasCertificate = true, Weight = 111, Id = 111};
                
                var validationRequest = _mapper.Map<KittenRequestValidation>(request);
                var result = await _kittenValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestKittensFromWebApiDto>(request);
                var response = await _kittenService.ReadKittensByParametersRequestToRepositoryAsync(requestToRepository);
                if (response is null)
                {
                    return BadRequest("Kitten doesn't exists");
                }
                
                _logger.LogInformation($"Kitten read process stops with success: {DateTimeOffset.Now}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateKittenByIdAsync(
            int kittenIdToEdit,
            string nickName,
            int weight,
            string color,
            string feedName,
            bool hasCertificate)
        {
            _logger.LogInformation($"Kitten updating process start: {DateTimeOffset.Now}");

            try
            {
                var request = new KittenRequestDto() { Id = kittenIdToEdit, NickName = nickName, Weight = weight, Color = color, FeedName = feedName, HasCertificate = hasCertificate, Page = 111, Size = 111};
                var validationRequest = _mapper.Map<KittenRequestValidation>(request);

                var result = await _kittenValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestKittensFromWebApiDto>(request);
                var res = _kittenService.UpdateKittenByIdRequestToRepositoryAsync(requestToRepository.Id, requestToRepository);
                if (res.Exception is not null)
                {
                    return BadRequest("Kitten hasn't been updated");
                }

                _logger.LogInformation($"Kitten updating process stop with success: {DateTimeOffset.Now}");

                return Ok("Kitten has been updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteKittenByIdAsync(int kittenIdToDelete)
        {
            _logger.LogInformation($"Kitten delete proccess start: {DateTimeOffset.Now}");
            try
            {
                var request = new KittenRequestDto() { Id = kittenIdToDelete, NickName = "Foo", Weight = 111, Color = "Foo", FeedName = "Foo", HasCertificate = true, Page = 111, Size = 111 };
                
                var validationRequest = _mapper.Map<KittenRequestValidation>(request);
                var result = await _kittenValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestKittensFromWebApiDto>(request);
                var res = await _kittenService.DeleteKittenByIdRequestToRepositoryAsync(requestToRepository);
                if (res.Exception is not null)
                {
                    return BadRequest("Kitten hasn't been deleted");
                }

                _logger.LogInformation($"Kitten delete proccess stop with success: {DateTimeOffset.Now}");

                return Ok("Kitten has been deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }
    }
}
