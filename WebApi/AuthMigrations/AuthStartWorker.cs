using System.Threading;
using System.Threading.Tasks;
using AuthDataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AuthMigrations
{
    public sealed class AuthStartWorker : IHostedService
    {
        private readonly IDbContextFactory<AuthDataContext> _applicationContext;
        private CancellationTokenSource _cts;
        
        public AuthStartWorker(IDbContextFactory<AuthDataContext> applicationContext)
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