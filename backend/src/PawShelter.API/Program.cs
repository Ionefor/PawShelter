<<<<<<< Updated upstream
=======
using FluentValidation;
using PawShelter.Application;
>>>>>>> Stashed changes
using PawShelter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
