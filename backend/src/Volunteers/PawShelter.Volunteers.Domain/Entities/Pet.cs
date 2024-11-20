using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects.ForPet;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Domain.Entities;

public class Pet : SoftDeletableEntity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(PetId id,
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
        Requisites requisites,
        PetStatus status)
        : base(id)
    {
        Name = name;
        Description = description;
        SpeciesBreedsId = speciesBreedsId;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        PetCharacteristics = petCharacteristics;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        Birthday = birthday;
        PublicationDate = publicationDate;
        Requisites = requisites;
        Status = status;
    }

    private List<PetPhoto> _photos = [];
    public IReadOnlyList<PetPhoto>? Photos => _photos;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public SpeciesBreedsId SpeciesBreedsId { get; private set; }
    public Color Color { get; private set; }
    public HealthInfo HealthInfo { get; private set; } = null!;
    public Address Address { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public PetCharacteristics PetCharacteristics { get; private set; }
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    public Position Position { get; private set; }
    public Birthday Birthday { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public Requisites Requisites { get; private set; }
    public PetStatus Status { get; private set; }
    
    internal void Move(Position position)
    {
        Position = position;
    }

    internal UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }
    internal UnitResult<Error> MoveBackward()
    {
        var newPosition = Position.Backward();
        if (newPosition.IsFailure)
            return newPosition.Error;
        
        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void UpdatePetPhotos(IEnumerable<PetPhoto> photos)
    {
       _photos = photos!.ToList();
    }

    public void UpdatePetStatus(PetStatus status)
    {
        Status = status;
    }

    public UnitResult<Error> SetMainPhoto(PetPhoto newPhoto)
    {
        var oldMainPhoto = _photos.
            FirstOrDefault(p => p.IsMain);
        if (oldMainPhoto is not null)
        {
            var oldPhoto = new PetPhoto(oldMainPhoto.Path, false);
            _photos.Add(oldPhoto);
            _photos.Remove(oldMainPhoto);
        }
        
        var photo = _photos.FirstOrDefault(
            p => p.Path == newPhoto.Path);
        
        if (photo is null)
        {
            return Error.NotFound(
                "photo.not.found", "photo not found");
        }

        _photos.Remove(photo);
        _photos.Add(newPhoto);
        
        _photos = _photos.
            OrderByDescending(p => p.IsMain).ToList();
        
        return Result.Success<Error>();
    }

    public void UpdatePet(
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
        Name = name;
        Description = description;
        SpeciesBreedsId = speciesBreedsId;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        PetCharacteristics = petCharacteristics;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        Birthday = birthday;
        PublicationDate = publicationDate;
        Requisites = requisites;
    }
}