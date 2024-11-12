using FluentValidation;
using PawShelter.Species.Application;
using PawShelter.Species.Infrastructure;
using PawShelter.Species.Presentation;
using PawShelter.Volunteers.Application;
using PawShelter.Volunteers.Infrastructure;
using PawShelter.Volunteers.Presentation;
using PawShelter.Web.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                 ?? throw new ArgumentNullException("Seq"))
    .Enrich.WithThreadId()
    .Enrich.WithThreadName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddHttpLogging(o => { o.CombineLogs = true; });

builder.Services.AddSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.
    AddVolunteersInfrastructure(builder.Configuration).
    AddVolunteersApplication().
    AddVolunteersPresentation();

builder.Services.
    AddSpeciesInfrastructure(builder.Configuration).
    AddSpeciesApplication().
    AddSpeciesPresentation();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add services to the container.

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExeptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // await app.ApplyMigrations();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();