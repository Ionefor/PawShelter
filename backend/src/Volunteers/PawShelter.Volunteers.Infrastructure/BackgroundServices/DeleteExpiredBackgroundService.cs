using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PawShelter.Volunteers.Infrastructure.BackgroundServices;

public class DeleteExpiredBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DeleteExpiredBackgroundService> _logger;

    public DeleteExpiredBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<DeleteExpiredBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteExpiredBackgroundService is starting.");

        while (!cancellationToken.IsCancellationRequested)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var deleteExpiredService = scope.ServiceProvider.
                GetRequiredService<DeleteExpiredService>();
            
            _logger.LogInformation("DeleteExpiredBackgroundService is running.");

            await deleteExpiredService.Process(cancellationToken);
            
            await Task.Delay(TimeSpan.FromHours(24), cancellationToken);
        }
    }
}