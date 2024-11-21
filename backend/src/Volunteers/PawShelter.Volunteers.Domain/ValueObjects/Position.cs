using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class Position : ComparableValueObject
{
    private Position(int value) =>  Value = value;
    public int Value { get; }
    public Result<Position, Error> Forward() => Create(Value + 1);
    public Result<Position, Error> Backward() => Create(Value - 1);
    public static implicit operator int(Position position) => position.Value;
    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Position)));
        }
        
        return new Position(number);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}