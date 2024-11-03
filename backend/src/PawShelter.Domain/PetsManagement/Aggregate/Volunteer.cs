using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.Aggregate;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private readonly List<Pet> _pets = [];
    private bool _isDeleted;

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phoneNumber,
        Experience experience,
        Requisites requisites,
        SocialNetworks socialNetworks)
        : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        Experience = experience;
        Requisites = requisites;
        SocialNetworks = socialNetworks;
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Experience Experience { get; private set; }
    public Requisites Requisites { get; private set; }
    public SocialNetworks SocialNetworks { get; private set; }
    public IReadOnlyList<Pet>? Pets => _pets;

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public int CountPetsByStatus(PetStatus status)
    {
        return _pets.Count(p => p.Status == status);
    }

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phoneNumber,
        Experience experience)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        Experience = experience;
    }

    public void UpdateRequisites(Requisites requisites)
    {
        Requisites = requisites;
    }

    public void UpdateSocialNetworks(SocialNetworks socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var positionToAddedPet = Position.Create(_pets.Count + 1);

        if (positionToAddedPet.IsFailure)
            return positionToAddedPet.Error;

        pet.Move(positionToAddedPet.Value);
        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition)
            return Result.Success<Error>();

        if (newPosition > _pets.Count || newPosition < 1)
            return UnitResult.Failure(
                Errors.Extra.InvalidPosition(newPosition));

        MovePetsBetweenPositions(newPosition, currentPosition);
        pet.Move(newPosition);

        return Result.Success<Error>();
    }

    private UnitResult<Error> MovePetsBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition < currentPosition)
        {
            var petsToMove = _pets.Where(p => p.Position >= newPosition && p.Position < currentPosition);

            foreach (var pet in petsToMove)
            {
                var result = pet.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition > currentPosition)
        {
            var petsToMove = _pets.Where(p => p.Position <= newPosition && p.Position > currentPosition);

            foreach (var pet in petsToMove)
            {
                var result = pet.MoveBackward();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }
}