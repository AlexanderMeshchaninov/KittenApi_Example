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
    public sealed class ClinicControllerUnitTest
    {
        private ClinicController _controller; 
        private readonly Mock<ILogger<ClinicController>> _logger;
        private readonly Mock<IOperationClinicService> _clinicValidator;
        private readonly Mock<IClinicService> _clinicService;

        public ClinicControllerUnitTest()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            _clinicValidator = new Mock<IOperationClinicService>();
            _logger = new Mock<ILogger<ClinicController>>();
            _clinicService = new Mock<IClinicService>();

            _controller = new ClinicController(
                _logger.Object, 
                mapper, 
                _clinicValidator.Object, 
                _clinicService.Object);
        }

        [Fact]
        public async Task RegisterClinic_Return_IActionResult_OK()
        {
            //Mock setup
            _clinicValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<ClinicRequestValidation>()).Result)
                .Returns(new OperationClinicResult(It.IsAny<ClinicRequestValidation>()));

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicName = "Foo",
            };

            //Act
            var result = await _controller.RegisterClinicAsync(request.ClinicName)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task RegisterKittenToClinic_Return_IActionResult_OK()
        {
            //Mock setup
            _clinicValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<ClinicRequestValidation>()).Result)
                .Returns(new OperationClinicResult(It.IsAny<ClinicRequestValidation>()));

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicId = 1,
                KittenId = 10,
            };

            //Act
            var result = await _controller.RegisterKittenToClinicAsync(request.ClinicId, request.KittenId)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ReadClinicByParameter_Return_IActionResult_OK()
        {
            //Mock setup
            _clinicValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<ClinicRequestValidation>()).Result)
                .Returns(new OperationClinicResult(It.IsAny<ClinicRequestValidation>()));

            _clinicService.Setup(x =>
                    x.ReadClinicByParameterRequestToRepositoryAsync(It.IsAny<RequestClinicsFromWebApiDto>()).Result)
                .Returns(new ResponseClinicsToWebApiDto()
                {
                    Content = new List<ClinicDTO>()
                });

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicName = "HappyCat",
            };

            //Act
            var result = await _controller.ReadClinicByParametersAsync(request.ClinicName, 1, 5)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateClinicById_Return_IActionResult_OK()
        {
            //Mock setup
            _clinicValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<ClinicRequestValidation>()).Result)
                .Returns(new OperationClinicResult(It.IsAny<ClinicRequestValidation>()));

            _clinicService.Setup(x =>
                x.UpdateClinicsByIdRequestToRepositoryAsync(It.IsAny<RequestClinicsFromWebApiDto>()).Result)
                .Returns(Task.CompletedTask);

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicId = 1,
                ClinicName = "NewFoo",
            };

            //Act
            var result = await _controller.UpdateClinicsById(request.ClinicId, request.ClinicName)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteClinic_Return_IActionResult_OK()
        {
            //Mock setup
            _clinicValidator.Setup(x =>
                    x.StartValidationAsync(It.IsAny<ClinicRequestValidation>()).Result)
                .Returns(new OperationClinicResult(It.IsAny<ClinicRequestValidation>()));

            _clinicService.Setup(x =>
                    x.DeleteClinicsByIdRequestToRepositoryAsync(It.IsAny<RequestClinicsFromWebApiDto>()).Result)
                .Returns(Task.CompletedTask);

            //Arrage
            var request = new ClinicRequestDto()
            {
                ClinicId = 1,
            };

            //Act
            var result = await _controller.DeleteClinicsById(request.ClinicId)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
