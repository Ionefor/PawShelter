﻿using Microsoft.EntityFrameworkCore;
using PawShelter.Infrastructure.DbContexts;

namespace PawShelter.API.Extensions;

public static class AppExtension
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}