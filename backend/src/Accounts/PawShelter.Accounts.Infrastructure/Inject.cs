using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.Accounts.Infrastructure.Options;
using PawShelter.Accounts.Infrastructure.Providers;

namespace PawShelter.Accounts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));

       services.AddOptions<JwtOptions>();

        services.AddIdentityCore<User>(options => { options.User.RequireUniqueEmail = true; }).
            AddRoles<Role>().
            AddEntityFrameworkStores<AccountDbContext>().
            AddDefaultTokenProviders();
        
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
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                };
            });
        
        services.AddAuthorization();
        
        services.AddScoped<AccountDbContext>();
        
        return services;
    }
}