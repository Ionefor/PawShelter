namespace PawShelter.SharedKernel.Models.Error;

public static class ErrorParameters
{
    public static class General
    {
        public record NotFound(string ObjectName, string ObjectTypeSearching, object  ObjectValueSearching);
        public record ValueIsRequired(string ObjectName);
        public record ValueIsInvalid(string ObjectName);
        public record InternalServer(string Message);
        public record Failed(string Message);
    }
    
    public static class Extra
    {
        public record PositionIsInvalid(int Position);
        public record InvalidDeleteOperation(string ObjectName, object ObjectValue);
        public record ValueAlreadyExists(string Message);
        public record RoleIsInvalid(string Message);
    }
}
