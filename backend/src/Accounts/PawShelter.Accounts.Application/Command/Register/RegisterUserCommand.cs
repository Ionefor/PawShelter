using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Application.Command.Register;

public record RegisterUserCommand(
    string Email, string UserName, 
    string Password, FullNameDto FullName) : ICommand;