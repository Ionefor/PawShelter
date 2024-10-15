using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Application.Files.Delete;
using PawShelter.Application.Files.Get;
using PawShelter.Application.Files.Upload;
using PawShelter.Application.Volunteers.Create;
using PawShelter.Application.Volunteers.Delete;
using PawShelter.Application.Volunteers.UpdateMainInfo;
using PawShelter.Application.Volunteers.UpdateRequisites;
using PawShelter.Application.Volunteers.UpdateSocialNetworks;

namespace PawShelter.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateRequisitesHandler>();
            services.AddScoped<UpdateSocialNetworksHandler>();
            services.AddScoped<DeleteVolunteerHandler>();
            services.AddScoped<UploadFileHandler>();
            services.AddScoped<DeleteFileHandler>();
            services.AddScoped<GetFileHandler>();
            
            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            
            return services;
        }
    }
}
