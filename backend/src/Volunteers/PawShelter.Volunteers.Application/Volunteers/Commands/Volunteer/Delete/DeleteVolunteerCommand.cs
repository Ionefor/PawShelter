using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;