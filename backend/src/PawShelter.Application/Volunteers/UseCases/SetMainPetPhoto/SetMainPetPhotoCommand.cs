using System.Windows.Input;
using ICommand = PawShelter.Application.Abstractions.ICommand;

namespace PawShelter.Application.Volunteers.UseCases.SetMainPetPhoto;

public record SetMainPetPhotoCommand(
    Guid VolunteerId, Guid PetId, string FilePath) : ICommand;