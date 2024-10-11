namespace PawShelter.Domain.Shared
{
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
                var forId = id is null ? "" : $"for Id {id}";
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
    }
}
