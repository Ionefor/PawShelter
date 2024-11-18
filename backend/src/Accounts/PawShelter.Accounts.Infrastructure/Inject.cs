using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.Authorization;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.Accounts.Infrastructure.Options;
using PawShelter.Accounts.Infrastructure.Providers;
using PawShelter.Accounts.Infrastructure.Seading;
using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddTransient<ITokenProvider, JwtTokenProvider>()
            .AddDbContext()
            .AddCustomAuthorization()
            .AddJwtOptions(configuration)
            .AddAdminOptins(configuration)
            .AddJwtBearer(configuration).
            AddDatabase();

        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services
            .AddScoped<AccountDbContext>()
            .AddIdentity<User, Role>(options => { options.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    
    private static IServiceCollection AddJwtOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));

        services.AddOptions<JwtOptions>();
        
        return services;
    }

    private static IServiceCollection AddAdminOptins(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AdminOptions>(
            configuration.GetSection(AdminOptions.ADMIN));

        services.AddOptions<AdminOptions>();
        
        return services;
    }
    
    private static IServiceCollection AddJwtBearer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                                 ?? throw new ApplicationException("Missing JWT configuration");
                
                options.TokenValidationParameters =
                    TokenValidationParametersFactory.CreateWithLifeTime(jwtOptions);
            });

        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>("Accounts");
        
        return services;
    }
    private static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {                                               
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAccountManager, AccountManager>();

        services.AddAuthorization();

        services.AddSingleton<AccountSeeder>();

        services.AddScoped<AccountsSeederService>();

        services.AddScoped<PermissionManager>();
        
        services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();
        
        services.AddScoped<RolePermissionManager>();
        
        return services;
    }
}