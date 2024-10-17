using Microsoft.AspNetCore.Http;

namespace PawShelter.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosRequest(IFormFileCollection Files);