using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

public class Position : ValueObject
{
    private Position(int value)
    {
        Value = value;
    }
    
    public int Value { get; }

    public Result<Position, Error> Forward() =>
        Create(Value + 1);
    
    public Result<Position, Error> Backward() =>
        Create(Value - 1);
    
    public static implicit operator int(Position position) =>
        position.Value;
    
    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
            return Errors.General.ValueIsInvalid("SerialNumber");
        
        return new Position(number);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}