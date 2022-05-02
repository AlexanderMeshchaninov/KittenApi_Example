using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiBusinessLayer.Requests;
using WebApiBusinessLayer.Responses;
using WebApiBusinessLayerAbstraction.ClientService;
using WebApiBusinessLayerAbstraction.Requests;
using WebApiDataLayer.Models;
using WebApiRepository;

namespace WebApiBusinessLayer
{
    public interface IKittenService :
        IClientKittenService<RequestKittensFromWebApiDto>,
        IReadKittensByParametersRequestToRepository<ResponseKittensToWebApiDTO>
    {
    }

    public sealed class ClientKittenService : IKittenService
    {
        private readonly IKittensRepository _repository;

        public ClientKittenService(IKittensRepository repository)
        {
            _repository = repository;
        }

        public async Task<Task> RegisterKittenRequestToRepositoryAsync(RequestKittensFromWebApiDto item)
        {
            var newKitten = new Kitten()
            {
                Id = 0, 
                NickName = item.NickName, 
                Weight = item.Weight, 
                Color = item.Color, 
                FeedName = item.FeedName,
                HasCertificate = item.HasCertificate
            };

            var res = await _repository.CreateAsync(newKitten);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<ResponseKittensToWebApiDTO> ReadKittensByParametersRequestToRepositoryAsync(IRequestKittensFromWebApiDto item)
        {
            var kitten = new Kitten()
            {
                NickName = item.NickName,
            };

            var fromRepo = await _repository.ReadByParameterAsync(kitten.NickName, item.Page, item.Size);
            if (fromRepo.Count == 0)
            {
                return null;
            }

            var response = new ResponseKittensToWebApiDTO()
            {
                Content = new List<KittenDTO>()
            };

            foreach (var kittens in fromRepo)
            {
                response.Content.Add(new KittenDTO()
                {
                    Id = kittens.Id,
                    NickName = kittens.NickName,
                    Weigth = kittens.Weight,
                    Color = kittens.Color,
                    FeedName = kittens.FeedName,
                    HasCertificate = kittens.HasCertificate
                });
            }

            return response;
        }

        public async Task<Task> UpdateKittenByIdRequestToRepositoryAsync(int id, RequestKittensFromWebApiDto item)
        {
            var newKitten = new Kitten()
            {
                NickName = item.NickName,
                Color = item.Color,
                FeedName = item.FeedName,
                HasCertificate = item.HasCertificate,
                Weight = item.Weight
            };

            var fromRepo = await _repository.UpdateAsync(item.Id, newKitten);
            if (fromRepo.Exception is not null)
            {
                return Task.FromException(fromRepo.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<Task> DeleteKittenByIdRequestToRepositoryAsync(RequestKittensFromWebApiDto item)
        {
            var kitten = new Kitten()
            {
                Id = item.Id,
            };

            var fromRepo = await _repository.DeleteAsync(kitten.Id);
            if (fromRepo.Exception is not null)
            {
                return Task.FromException(fromRepo.Exception);
            }

            return Task.CompletedTask;
        }
    }
}