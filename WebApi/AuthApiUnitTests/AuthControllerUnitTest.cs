using System.Threading.Tasks;
using AuthApi;
using Xunit;
using AuthApi.Controllers;
using AuthApi.UserRequest;
using AuthApiFluentValidation.Models;
using AuthApiFluentValidation.Services;
using AuthBusinessLayer;
using AuthBusinessLayer.Requests;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AuthApiUnitTests
{
    public sealed class AuthControllerUnitTest
    {
        private AuthController _controller;
        private readonly Mock<ILogger<AuthController>> _logger;
        private readonly Mock<IOperationService> _authValidator;
        private readonly Mock<IClientService> _clientService;

        public AuthControllerUnitTest()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            _authValidator = new Mock<IOperationService>();
            _logger = new Mock<ILogger<AuthController>>();
            _clientService = new Mock<IClientService>();

            _controller = new AuthController(_logger.Object, mapper, _authValidator.Object, _clientService.Object);
        }

        [Fact]
        public async Task UserRegistration_Returns_IActionResult_OK()
        {
            //Mock setup
            _authValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<UserRequestValidation>()).Result)
                .Returns(new OperationResult(It.IsAny<UserRequestValidation>()));

            //Arrage
            var request = new UserRequestDto()
            {
                UserName = "Vasiliy",
                Email = "vasiliy@mail.ru",
                Password = "12345678"
            };

            //Act
            var result = await _controller.UserRegistrationAsync(request.UserName, request.Email, request.Password)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task RequestToUserValidator_Return_SuccessTrue()
        {
            //Mock setup
            _authValidator.Setup(x =>
                x.StartValidationAsync(It.IsAny<UserRequestValidation>()).Result)
                .Returns(new OperationResult(It.IsAny<UserRequestValidation>()));

            //Arrange
            var request = new UserRequestValidation()
            {
                UserName = "Test",
                Email = "test@mail.ru",
                Password = "123",
            };

            //Act
            var result = await _authValidator.Object.StartValidationAsync(request).ConfigureAwait(false);
            var okResult = result.Succeed;

            //Assert
            Assert.Equal(true, okResult);
        }

        [Fact]
        public async Task RegisterUserRequestToRepository_Return_NoTaskException()
        {
            //Mock setup
            _authValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<UserRequestValidation>()).Result)
                .Returns(new OperationResult(It.IsAny<UserRequestValidation>()));

            //Arrange
            var request = new RequestFromAuthApiDto()
            {
                UserName = "Test",
                Email = "test@mail.ru",
                Password = "123",
            };

            //Act
            var result = await _clientService.Object.RegisterUserRequestToRepositoryAsync(request).ConfigureAwait(false);
            var okResult = result.Exception;

            //Assert
            Assert.Equal(null, okResult);
        }

        [Fact]
        public async Task UserLogin_Return_BadRequest_OK()
        {
            //Mock setup
            _authValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<UserRequestValidation>()).Result)
                .Returns(new OperationResult(It.IsAny<UserRequestValidation>()));
            
            _clientService.Setup(x =>
                    x.AuthenticationRequestAsync(It.IsAny<RequestFromAuthApiDto>())).Verifiable();

            //Arrange
            var request = new UserRequestDto()
            {
                UserName = "Foo",
                Email = "peter@mail.ru",
                Password = "12345678"
            };

            //Act
            var result = await _controller.UserLogin(request.Email, request.Password).ConfigureAwait(false);
            var okResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
