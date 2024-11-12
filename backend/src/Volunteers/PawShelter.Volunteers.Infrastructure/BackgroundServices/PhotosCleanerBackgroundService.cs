﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Messaging;
using PawShelter.Volunteers.Application.PhotoProvider;

namespace PawShelter.Volunteers.Infrastructure.BackgroundServices;

public class PhotosCleanerBackgroundService(
    IServiceScopeFactory scopeFactory,
    IMessageQueue<IEnumerable<PhotoMetaData>> messageQueue,
    ILogger<PhotosCleanerBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting PhotosCleanerBackgroundService");

        await using var scope = scopeFactory.CreateAsyncScope();

        var photoProvider = scope.ServiceProvider.GetRequiredService<IPhotoProvider>();

        while (!cancellationToken.IsCancellationRequested)
        {
            var photoInfos = await messageQueue.ReadAsync(cancellationToken);

            foreach (var photoInfo in photoInfos) await photoProvider.DeleteFile(photoInfo, cancellationToken);
        }

        await Task.CompletedTask;
    }
}