using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Abstractions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Domain.Entities;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Domain.Aggregate;

public class Volunteer : SoftDeletableEntity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private Volunteer(VolunteerId id) : base(id) {}

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
    public IReadOnlyList<Pet> Pets => _pets;
    
    public override void Delete()
    {
        base.Delete();

        foreach (var pet in _pets)
            pet.Delete();
    }

    public override void Restore()
    {
        base.Restore();

        foreach (var pet in _pets)
            pet.Restore();
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
        {
            return UnitResult.
                Failure(Errors.Extra.
                    PositionIsInvalid(
                        new ErrorParameters.Extra.PositionIsInvalid(newPosition)));
        }

        MovePetsBetweenPositions(newPosition, currentPosition);
        pet.Move(newPosition);

        return Result.Success<Error>();
    }

    private UnitResult<Error> MovePetsBetweenPositions(
        Position newPosition, Position currentPosition)
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
    
    public void HardDeletePet(Pet pet)
    {
        UpdatePositionPet(pet);
        _pets.Remove(pet);
    }
    public void SoftDeletePet(Pet pet)
    {
        UpdatePositionPet(pet);

        _pets[pet.Position - 1].Delete();
    }

    private void UpdatePositionPet(Pet pet)
    {
        for (int i = pet.Position; i < _pets.Count; i++)
        {
            _pets[i].MoveBackward();
        }
    }
    
    public void UpdatePet(
        Pet pet,
        Name name,
        Description description,
        SpeciesBreedsId speciesBreedsId,
        Color color,
        HealthInfo healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        PetCharacteristics petCharacteristics,
        bool isCastrated,
        bool isVaccinated,
        Birthday birthday,
        DateTime publicationDate,
        Requisites requisites)
    {
        _pets[pet.Position - 1].UpdatePet(
            name, description, speciesBreedsId, color,
            healthInfo, address, phoneNumber,
            petCharacteristics, isCastrated, isVaccinated,
            birthday, publicationDate, requisites);
    }
    
    public UnitResult<Error> SetMainPetPhoto(Pet pet, PetPhoto newPhoto)
    {
        return _pets[pet.Position - 1].SetMainPhoto(newPhoto);
    }
    
    public void UpdatePetStatus(Pet pet, PetStatus status)
    {
        _pets[pet.Position - 1].UpdateStatus(status);
    }
    
    public void UpdatePetPhotos(Pet pet, IEnumerable<PetPhoto> photos)
    {
        _pets[pet.Position - 1].UpdatePhotos(photos);
    }
}