using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDataLayer;
using WebApiRepositoryAbstraction;
using WebApiDataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiRepositoryAbstraction.Interfaces;

namespace WebApiRepository
{
    public interface IClinicsRepository : 
        IClinicRepository<Clinic>, 
        IClinicReadRepository<Clinic>
    {
    }

    public sealed class ClinicRepository : IClinicsRepository
    {
        private readonly ILogger<ClinicRepository> _logger;
        private readonly WebApiDataContext _context;

        public ClinicRepository(ILogger<ClinicRepository> logger, WebApiDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Task> CreateClinicAsync(Clinic newClinic)
        {
            using (var db = _context)
            {
                try
                {
                    await db.Clinics.AddAsync(newClinic);
                    await db.SaveChangesAsync();

                    _logger.LogInformation(
                        $"Clinic has been added to DB with id: {newClinic?.Id} name: {newClinic?.ClinicName}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromException(ex);
                }

                return Task.CompletedTask;
            }
        }
        
        public async Task<Task> CreateKittenToClinicAsync(int clinicId, int kittenId)
        {
            using (var db = _context)
            {
                try
                {
                    var findClinic = await db.Clinics
                        .Where(x => x.Id.Equals(clinicId))
                        .SingleOrDefaultAsync();

                    var findKitten = await db.Kittens
                        .Where(x => x.Id.Equals(kittenId))
                        .SingleOrDefaultAsync();

                    await Task.Run(() => findClinic?.Kittens.Add(findKitten));

                    await db.SaveChangesAsync();

                    _logger.LogInformation(
                        $"Kitten has been added to Clinic with id: {findClinic?.Id} name: {findClinic?.ClinicName}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromException(ex);
                }
            }

            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Clinic>> ReadByParameterAsync(string clinicName, int page, int size)
        {
            using (var db = _context)
            {
                try
                {
                    return await db.Clinics.Include(x => x.Kittens)
                        .Where(x => x.ClinicName.Contains(clinicName))
                        .Skip((page - 1) * size)
                        .Take(size)
                        .AsNoTracking()
                        .ToArrayAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return null;
                }
            }
        }

        public async Task<Task> UpdateAsync(int clinicId, string newClinicName)
        {
            using (var db = _context)
            {
                var findItem = await db.Clinics
                    .Where(x => x.Id.Equals(clinicId))
                    .FirstOrDefaultAsync();

                if (findItem != null)
                {
                    try
                    {
                        findItem.ClinicName = newClinicName;

                        await Task.Run(() => db.Clinics.Update(findItem));

                        await db.SaveChangesAsync();

                        _logger.LogInformation($"Clinic has been updated");

                        return Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Exception:{ex}");
                        return Task.FromException(ex);
                    }
                }

                return Task.FromException(new NullReferenceException());
            }
        }

        public async Task<Task> DeleteAsync(int clinicId)
        {
            using (var db = _context)
            {
                try
                {
                    var deleteItem = await db.Clinics.Include(x => x.Kittens)
                        .FirstOrDefaultAsync(x => x.Id == clinicId);

                    if (deleteItem != null)
                    {
                        await Task.Run(() => db.Clinics.Remove(deleteItem));

                        await db.SaveChangesAsync();

                        _logger.LogInformation($"Item(Clinic) has been deleted");

                        return Task.CompletedTask;
                    }

                    return Task.FromException(new NullReferenceException());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromException(ex);
                }
            }
        }
    }
}