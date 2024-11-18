using FluentValidation;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.OpenApi.Models;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Infrastructure;
using PawShelter.Accounts.Infrastructure.Seading;
using PawShelter.Accounts.Presentation;
using PawShelter.Species.Application;
using PawShelter.Species.Infrastructure;
using PawShelter.Species.Presentation;
using PawShelter.Volunteers.Application;
using PawShelter.Volunteers.Infrastructure;
using PawShelter.Volunteers.Presentation;
using PawShelter.Web.Middlewares;
using Serilog;
using Serilog.Events;

DotNetEnv.Env.Load();
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

builder.Services.
    AddAccountsInfrastructure(builder.Configuration).
    AddAccountsApplication().
    AddAccountsPresentation();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PawShelter API",
        Version = "1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert JWT token value",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement

    { 
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Add services to the container.

var app = builder.Build();

var accountSeeder = app.Services.GetRequiredService<AccountSeeder>();

await accountSeeder.SeedAsync();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();