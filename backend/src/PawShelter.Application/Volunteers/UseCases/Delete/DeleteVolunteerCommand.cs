using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.UseCases.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;