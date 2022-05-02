using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi;
using WebApi.Controllers;
using WebApi.Requests;
using WebApiBusinessLayer;
using WebApiBusinessLayer.Requests;
using WebApiBusinessLayer.Responses;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;
using Xunit;

namespace WebApiUnitTests
{
    public sealed class KittensControllerUnitTest
    {
        private KittensController _controller; 
        private readonly Mock<ILogger<KittensController>> _logger;
        private readonly Mock<IOperationKittenService> _kittenValidator;
        private readonly Mock<IKittenService> _kittenService;

        public KittensControllerUnitTest()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            _kittenValidator = new Mock<IOperationKittenService>();
            _logger = new Mock<ILogger<KittensController>>();
            _kittenService = new Mock<IKittenService>();

            _controller = new KittensController(
                mapper, 
                _logger.Object, 
                _kittenValidator.Object, 
                _kittenService.Object);
        }

        [Fact]
        public async Task RegisterClinic_Return_IActionResult_OK()
        {
            //Mock setup
            _kittenValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<KittenRequestValidation>()).Result)
                .Returns(new OperationKittenResult(It.IsAny<KittenRequestValidation>()));

            //Arrage
            var request = new KittenRequestDto()
            {
                NickName = "Vasiliy",
                Weight = 3,
                Color = "Black",
                FeedName = "Whiskas",
                HasCertificate = true,
            };

            //Act
            var result = await _controller.RegisterKittenInfoAsync(
                    request.NickName, 
                    request.Weight, 
                    request.Color, 
                    request.FeedName, 
                    request.HasCertificate)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ReadKittenByParameter_Return_IActionResult_OK()
        {
            //Mock setup
            _kittenValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<KittenRequestValidation>()).Result)
                .Returns(new OperationKittenResult(It.IsAny<KittenRequestValidation>()));

            _kittenService.Setup(x =>
                    x.ReadKittensByParametersRequestToRepositoryAsync(It.IsAny<RequestKittensFromWebApiDto>()).Result)
                .Returns(new ResponseKittensToWebApiDTO()
                {
                    Content = new List<KittenDTO>()
                });

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicName = "HappyCat",
            };

            //Act
            var result = await _controller.ReadKittensByParametersAsync(request.ClinicName, 1, 5)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateKittenById_Return_IActionResult_OK()
        {
            //Mock setup
            _kittenValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<KittenRequestValidation>()).Result)
                .Returns(new OperationKittenResult(It.IsAny<KittenRequestValidation>()));

            _kittenService.Setup(x =>
                x.UpdateKittenByIdRequestToRepositoryAsync(It.IsAny<int>(), It.IsAny<RequestKittensFromWebApiDto>()).Result)
                .Returns(Task.CompletedTask);

            //Arrage
            var request = new KittenRequestDto()
            {
                Id = 1,
                NickName = "Vasiliy",
                Weight = 3,
                Color = "Black",
                FeedName = "Whiskas",
                HasCertificate = true,
            };

            //Act
            var result = await _controller.UpdateKittenByIdAsync(
                    request.Id, 
                    request.NickName,
                    request.Weight,
                    request.Color,
                    request.FeedName,
                    request.HasCertificate)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteKittenById_Return_IActionResult_OK()
        {
            //Mock setup
            _kittenValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<KittenRequestValidation>()).Result)
                .Returns(new OperationKittenResult(It.IsAny<KittenRequestValidation>()));

            _kittenService.Setup(x =>
                    x.DeleteKittenByIdRequestToRepositoryAsync(It.IsAny<RequestKittensFromWebApiDto>()).Result)
                .Returns(Task.CompletedTask);

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicId = 1,
            };

            //Act
            var result = await _controller.DeleteKittenByIdAsync(request.ClinicId)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
