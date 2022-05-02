using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBusinessLayer.Form;
using WebApiBusinessLayer.Requests;
using WebApiBusinessLayer.Responses;
using WebApiBusinessLayerAbstraction.ClientService;
using WebApiBusinessLayerAbstraction.Requests;
using WebApiDataLayer.Models;
using WebApiRepository;

namespace WebApiBusinessLayer
{
    public interface IClinicService : 
        IClientClinicService<RequestClinicsFromWebApiDto>,
        IReadClinicByParameterRequestToRepository<ResponseClinicsToWebApiDto>,
        IClientMedicalService<RequestClinicServiceFromWebApiDto>
    {
    }

    public sealed class ClientClinicService : IClinicService
    {
        private readonly IClinicsRepository _clinicRepository;
        private readonly IKittensRepository _kittensRepository;

        public ClientClinicService(
            IClinicsRepository clinicRepository,
            IKittensRepository kittensRepository)
        {
            _clinicRepository = clinicRepository;
            _kittensRepository = kittensRepository;
        }

        public async Task<Task> RegisterClinicRequestToRepositoryAsync(RequestClinicsFromWebApiDto item)
        {
            var newClinic = new Clinic()
            {
                Id = 0,
                ClinicName = item.ClinicName
            };

            var res = await _clinicRepository.CreateClinicAsync(newClinic);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<Task> RegisterKittenToClinicRequestToRepositoryAsync(RequestClinicsFromWebApiDto item)
        {
            //Этот блок чтобы соблюсти феншуй, можно конечно без него передать напрямую
            var clinic = new Clinic() { Id = item.ClinicId};
            var kitten = new Kitten() { Id = item.KittenId};

            var res = await _clinicRepository.CreateKittenToClinicAsync(clinic.Id, kitten.Id);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<ResponseClinicsToWebApiDto> ReadClinicByParameterRequestToRepositoryAsync(IRequestClinicsFromWebApiDto item)
        {
            var clinic = new Clinic(){ClinicName = item.ClinicName};

            var fromRepo = await _clinicRepository.ReadByParameterAsync(clinic.ClinicName, item.Page, item.Size);
            if (fromRepo.Count == 0)
            {
                return null;
            }

            var response = new ResponseClinicsToWebApiDto()
            {
                Content = new List<ClinicDTO>()
            };

            foreach (var clinics in fromRepo)
            {
                response.Content.Add(new ClinicDTO()
                {
                    Id = clinics.Id,
                    ClinicName = clinics.ClinicName,
                    Kittens = clinics.Kittens.ToList()
                });
            }

            return response;
        }

        public async Task<Task> UpdateClinicsByIdRequestToRepositoryAsync(RequestClinicsFromWebApiDto item)
        {
            var clinic = new Clinic() { Id = item.Id, ClinicName = item.ClinicName};

            var res = await _clinicRepository.UpdateAsync(clinic.Id, clinic.ClinicName);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<Task> DeleteClinicsByIdRequestToRepositoryAsync(RequestClinicsFromWebApiDto item)
        {
            var clinic = new Clinic() { Id = item.Id};

            var res = await _clinicRepository.DeleteAsync(clinic.Id);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<Task> MedicalProcedureRequestAsync(RequestClinicServiceFromWebApiDto item)
        {
            var kitten = new Kitten() {Id = item.KittenId};
            var fromRepo = _kittensRepository.FindKittenAsync(kitten.Id);
            if (fromRepo.Result is null)
            {
                return Task.FromException(new NullReferenceException());
            }

            foreach (var findKitten in fromRepo.Result)
            {
                if (findKitten.Clinics is null)
                {
                    return Task.FromResult("Kitten hasn't added to Clinic");
                }

                var medicalSheet = new ClinicMedicalForm()
                {
                    KittenId = findKitten.Id,
                    MedicalProcedure = item.MedicalProcedure
                };

                medicalSheet.Approved();

                var kittenInspected = new Kitten()
                {
                    HasMedicalInspection = true,
                    LastInspection = DateTimeOffset.Now
                };

                var res = await _kittensRepository.UpdateKittenMedicalInspectionAsync(findKitten.Id, kittenInspected);
                if (res.Exception is not null)
                {
                    return Task.FromException(res.Exception);
                }
            }

            return Task.CompletedTask;
        }
    }
}