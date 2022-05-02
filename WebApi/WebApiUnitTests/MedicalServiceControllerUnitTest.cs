using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi;
using WebApi.Controllers;
using WebApi.Requests;
using WebApiBusinessLayer;
using WebApiBusinessLayer.Responses;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;
using Xunit;

namespace WebApiUnitTests
{
    public sealed class MedicalServiceControllerUnitTest
    {
        private MedicalServicesController _controller; 
        private readonly Mock<ILogger<MedicalServicesController>> _logger;
        private readonly Mock<IOperationClinicMedService> _clinicValidator;
        private readonly Mock<IClinicService> _clinicService;

        public MedicalServiceControllerUnitTest()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            _clinicValidator = new Mock<IOperationClinicMedService>();
            _logger = new Mock<ILogger<MedicalServicesController>>();
            _clinicService = new Mock<IClinicService>();

            _controller = new MedicalServicesController(
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
                    x.StartValidationAsync(It.IsAny<ClinicServiceRequestValidation>()).Result)
                .Returns(new OperationClinicServiceResult(It.IsAny<ClinicServiceRequestValidation>()));

            //Arrage
            var request = new ClinicServiceRequestDto()
            {
                KittenId = 1,
                MedicalProcedure = "Grooming",
            };

            //Act
            var result = await _controller.AddServicesToKittensAsync(request.KittenId, request.MedicalProcedure)
                .ConfigureAwait(false);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
