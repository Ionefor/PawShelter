namespace PawShelter.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id is null ? "" : $" for Id {id}";
            return Error.NotFound("record.is.invalid", $"record not found{forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name is null ? " " : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length");
        }

        public static Error InternalServer(string message)
        {
            return Error.Failure("server.internal", message);
        }
    }

    public static class Extra
    {
        public static Error InvalidPosition(int? value = null)
        {
            var position = value is null ? "" : $"{value} ";
            return Error.NotFound("position.is.invalid", $"position {value}does not exist");
        }
        
        public static Error InvalidDeleteOperation(Guid? id = null, string? name = null)
        {
            var forId = id is null ? "" : $" for Id {id}";
            name = name is null ? " " : " " + name + " ";
            
            return Error.Conflict("cannot.delete",
                $"Cannot delete {name} {forId} because animals has this {name}");
        }
        
        
    }
}