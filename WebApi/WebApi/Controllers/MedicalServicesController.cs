using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebApi.Requests;
using WebApiBusinessLayer;
using WebApiBusinessLayer.Requests;
using WebApiBusinessLayer.Responses;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/medicalservice")]
    [ApiController]
    public sealed class MedicalServicesController : ControllerBase
    {
        private readonly ILogger<MedicalServicesController> _logger;
        private readonly IMapper _mapper;
        private readonly IOperationClinicMedService _clinicValidator;
        private readonly IClinicService _clinicService;

        public MedicalServicesController(
            ILogger<MedicalServicesController> logger,
            IMapper mapper,
            IOperationClinicMedService clinicValidator,
            IClinicService clinicService)
        {
            _logger = logger;
            _mapper = mapper;
            _clinicService = clinicService;
            _clinicValidator = clinicValidator;
        }

        [HttpPost("addclinicservices")]
        public async Task<IActionResult> AddServicesToKittensAsync(int kittenId, string medicalProcedure)
        {
            _logger.LogInformation($"Add medical service to kitten process start: {DateTimeOffset.Now}");

            try
            {
                var request = new ClinicServiceRequestDto() { KittenId = kittenId, MedicalProcedure = medicalProcedure };

                var validationRequest = _mapper.Map<ClinicServiceRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicServiceFromWebApiDto>(request);

                var toRepo = _clinicService.MedicalProcedureRequestAsync(requestToRepository);
                if (toRepo.Exception is not null)
                {
                    return BadRequest("Kitten hasn't been added to clinics");
                }

                _logger.LogInformation($"Add medical service to kitten process stops with success: {DateTimeOffset.Now}");

                return Ok("Kitten has been inspected successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }
    }
}
