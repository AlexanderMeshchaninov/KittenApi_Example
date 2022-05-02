using System.Threading;
using System.Threading.Tasks;
using WebApiDataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebApiMigrations
{
    public class StartWorker : IHostedService
    {
        private readonly IDbContextFactory<WebApiDataContext> _applicationContext;
        private CancellationTokenSource _cts;
        
        public StartWorker(IDbContextFactory<WebApiDataContext> applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            
            using (var dbContext = _applicationContext.CreateDbContext())
            {
                await dbContext.Database.MigrateAsync(_cts.Token);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            return Task.CompletedTask;
        }
    }
}