using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApi.Requests;
using WebApiBusinessLayer;
using WebApiBusinessLayer.Requests;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/clinics")]
    [ApiController]
    public sealed class ClinicController : ControllerBase
    {
        private readonly ILogger<ClinicController> _logger;
        private readonly IMapper _mapper;
        private readonly IOperationClinicService _clinicValidator;
        private readonly IClinicService _clinicService;

        public ClinicController(
            ILogger<ClinicController> logger,
            IMapper mapper,
            IOperationClinicService clinicValidator,
            IClinicService clinicService)
        {
            _logger = logger;
            _mapper = mapper;
            _clinicValidator = clinicValidator;
            _clinicService = clinicService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterClinicAsync(string clinicName)
        {
            _logger.LogInformation($"Clinic registration process start: {DateTimeOffset.Now}");

            try
            {
                var request = new ClinicRequestDto() { ClinicName = clinicName, ClinicId = 111, Page = 111, Size = 111, KittenId = 111, Id = 111};

                var validationRequest = _mapper.Map<ClinicRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicsFromWebApiDto>(request);
                var toRepo = await _clinicService.RegisterClinicRequestToRepositoryAsync(requestToRepository);
                if (toRepo.Exception is not null)
                {
                    return BadRequest("Clinic doesn't registered");
                }

                _logger.LogInformation($"Clinic registration process stops with success: {DateTimeOffset.Now}");

                return Ok("Clinic has been added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpPost("addkittentoclinic")]
        public async Task<IActionResult> RegisterKittenToClinicAsync(int clinicId, int kittenId)
        {
            _logger.LogInformation($"Add Kitten to Clinic process start: {DateTimeOffset.Now}");

            try
            {
                var request = new ClinicRequestDto() { ClinicId = clinicId, KittenId = kittenId, ClinicName = "Foo", Id = 111, Page = 111, Size = 111};
                
                var validationRequest = _mapper.Map<ClinicRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicsFromWebApiDto>(request);
                var toRepo = await _clinicService.RegisterKittenToClinicRequestToRepositoryAsync(requestToRepository);
                if (toRepo.Exception is not null)
                {
                    return BadRequest("Kitten hasn't been added to clinics");
                }

                _logger.LogInformation($"Add Kitten to Clinic process stops with success: {DateTimeOffset.Now}");

                return Ok("Kitten add to Clinic successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpGet("readbyparameters")]
        public async Task<IActionResult> ReadClinicByParametersAsync(
            string clinicName, 
            int page, 
            int size)
        {
            _logger.LogInformation($"Clinic reading process start: {DateTimeOffset.Now}");

            try
            {
                var request = new ClinicRequestDto() { ClinicName = clinicName, Page = page, Size = size, ClinicId = 111, KittenId = 111, Id = 111};
                
                var validationRequest = _mapper.Map<ClinicRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicsFromWebApiDto>(request);
                var response = await _clinicService.ReadClinicByParameterRequestToRepositoryAsync(requestToRepository);
                if (response is null)
                {
                    return BadRequest("Clinics doesn't exists");
                }

                _logger.LogInformation($"Clinic reading process stops with success: {DateTimeOffset.Now}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpPut("updatebyid")]
        public async Task<IActionResult> UpdateClinicsById(int clinicId, string newClinicName)
        {
            _logger.LogInformation($"Clinic updating process start: {DateTimeOffset.Now}");

            try
            {
                var request = new ClinicRequestDto() { Id = clinicId, ClinicName = newClinicName, KittenId = 111, Page = 111, Size = 111, ClinicId = 111};

                var validationRequest = _mapper.Map<ClinicRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicsFromWebApiDto>(request);
                var res = await _clinicService.UpdateClinicsByIdRequestToRepositoryAsync(requestToRepository);
                if (res.Exception is not null)
                {
                    return BadRequest("Clinic hasn't been updated");
                }

                return Ok("Clinic has been updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex}");
                return UnprocessableEntity(ex);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClinicsById(int idClinicToDelete)
        {
            _logger.LogInformation($"Clinic delete process start: {DateTimeOffset.Now}");
            try
            {
                var request = new ClinicRequestDto() { Id = idClinicToDelete, ClinicName = "Foo", ClinicId = 111, KittenId = 111, Page = 111, Size = 111};
                
                var validationRequest = _mapper.Map<ClinicRequestValidation>(request);
                var result = await _clinicValidator.StartValidationAsync(validationRequest);
                if (!result.Succeed)
                {
                    return UnprocessableEntity(result.Failures);
                }

                var requestToRepository = _mapper.Map<RequestClinicsFromWebApiDto>(request);
                var toRepo = await _clinicService.DeleteClinicsByIdRequestToRepositoryAsync(requestToRepository);
                if (toRepo.Exception is not null)
                {
                    return BadRequest("Clinic hasn't been updated");
                }

                _logger.LogInformation($"Clinic delete process stop with success: {DateTimeOffset.Now}");

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
