using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDataLayer;
using WebApiDataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiRepositoryAbstraction.Interfaces;

namespace WebApiRepository
{
    public interface IKittensRepository :
        IKittenRepository<Kitten>,
        IKittenReadRepository<Kitten>,
        IFindKittenRepository<Kitten>
    {
    }

    public sealed class KittenRepository : IKittensRepository
    {
        private readonly ILogger<KittenRepository> _logger;
        private readonly WebApiDataContext _webApiDataContext;

        public KittenRepository(
            ILogger<KittenRepository> logger,
            WebApiDataContext webApiDataContext)
        {
            _webApiDataContext = webApiDataContext;
            _logger = logger;
        }

        public async Task<Task> CreateAsync(Kitten item)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    await db.Kittens.AddAsync(item);
                    await db.SaveChangesAsync();

                    _logger.LogInformation(
                        $"Kitten has been added to DB with id: {item.Id.ToString()}, name: {item.NickName}");

                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromResult(ex);
                }
            }
        }

        public async Task<IReadOnlyList<Kitten>> ReadByParameterAsync(string name, int page, int size)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    return await db.Kittens
                        .Include(x => x.Clinics)
                        .Where(n => n.NickName.Contains(name))
                        .Skip((page - 1) * size)
                        .Take(size)
                        .AsNoTracking()
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return null;
                }
            }
        }

        public async Task<Task> UpdateAsync(int id, Kitten kitten)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    var findItem = await db.Kittens
                        .Where(x => x.Id.Equals(id))
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (findItem != null)
                    {
                        findItem.NickName = kitten.NickName;
                        findItem.Weight = kitten.Weight;
                        findItem.Color = kitten.Color;
                        findItem.FeedName = kitten.FeedName;
                        findItem.HasCertificate = kitten.HasCertificate;

                        await Task.Run(() => db.Kittens.Update(findItem));
                        await db.SaveChangesAsync();

                        _logger.LogInformation($"Kitten has been updated");

                        return Task.CompletedTask;
                    }

                    return Task.FromException(new NullReferenceException());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromResult(ex);
                }
            }
        }

        public async Task<Task> UpdateKittenMedicalInspectionAsync(int id, Kitten item)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    var findItem = await db.Kittens
                        .Where(x => x.Id.Equals(id))
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (findItem != null)
                    {
                        findItem.HasMedicalInspection = item.HasMedicalInspection;
                        findItem.LastInspection = item.LastInspection;

                        await Task.Run(() => db.Kittens.Update(findItem));
                        await db.SaveChangesAsync();

                        _logger.LogInformation($"Kitten has been updated");

                        return Task.CompletedTask;
                    }

                    return Task.FromException(new NullReferenceException());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromResult(ex);
                }
            }
        }

        public async Task<Task> DeleteAsync(int id)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    var deleteItem = await db.Kittens
                        .Include(x => x.Clinics)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (deleteItem != null)
                    {
                        await Task.Run(() => db.Kittens.Remove(deleteItem));
                        await db.SaveChangesAsync();

                        _logger.LogInformation($"Kitten has been deleted");

                        return Task.CompletedTask;
                    }

                    return Task.FromException(new NullReferenceException());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromResult(ex);
                }
            }
        }

        public async Task<IReadOnlyList<Kitten>> FindKittenAsync(int id)
        {
            using (var db = _webApiDataContext)
            {
                try
                {
                    return await db.Kittens
                        .Include(x => x.Clinics)
                        .Where(x => x.Id.Equals(id))
                        .AsNoTracking()
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return null;
                }
            }
        }
    }
}


    