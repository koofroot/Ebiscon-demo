
using EbisconDemo.Services.Interfaces;

namespace EbisconDemo.Api.HostedServices
{
    public class DailyProductSyncService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope scope;

        private Timer timer;

        public DailyProductSyncService(IServiceProvider serviceProvide)
        {
            _serviceProvider = serviceProvide;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            scope = _serviceProvider.CreateScope();
            var syncService = scope.ServiceProvider.GetRequiredService<ISynchronizationService>();

            timer = new Timer(async _ => await syncService.SyncProductsAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(100));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
            scope?.Dispose();
        }
    }
}
