using PawShelter.SharedKernel.Definitions;

namespace PawShelter.SharedKernel.Models.Error;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(ErrorParameters.General.ValueIsInvalid? parameters = null)
        {
            if (parameters is null)
            {
                return Error.Validation(
                        Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsGeneralMessage.ValueIsInvalid);
            }
            
            return Error.Validation(
                    Constants.ErrorsCode.ValueIsInvalid, $"{parameters.ObjectName} is invalid");
        }
        public static Error NotFound(ErrorParameters.General.NotFound? parameters = null)
        {
            if (parameters is null)
            {
                return Error.
                    NotFound(Constants.ErrorsCode.NotFound, Constants.ErrorsGeneralMessage.NotFound);
            }
            
            return Error.
                NotFound(
                    Constants.ErrorsCode.NotFound,
                    $"{parameters.ObjectName} record not found with " + 
                    $"{parameters.ObjectTypeSearching} : " +
                    $"{parameters.ObjectValueSearching}");
        }
        public static Error ValueIsRequired(ErrorParameters.General.ValueIsRequired? parameters = null)
        {
            if (parameters is null)
            {
                return Error.
                    Validation(Constants.ErrorsCode.ValueIsRequired,
                        Constants.ErrorsGeneralMessage.ValueIsRequired);
            }
            
            return Error.
                Validation(Constants.ErrorsCode.ValueIsRequired,
                    $"{parameters.ObjectName} cannot be null or empty");
        }
        public static Error InternalServer(ErrorParameters.General.InternalServer? parameters = null)
        {
            if (parameters is null)
            {
                return Error.
                    Failure(Constants.ErrorsCode.InternalServer, Constants.ErrorsGeneralMessage.InternalServer);
            }
                
            return Error.
                Failure(Constants.ErrorsCode.InternalServer, parameters.Message);
        }
        public static Error Failed(ErrorParameters.General.Failed? parameters = null)
        {
            if (parameters is null)
            {
                return Error.
                    Failure(Constants.ErrorsCode.Failed, Constants.ErrorsGeneralMessage.Failed);
            }
                
            return Error.
                Failure(Constants.ErrorsCode.Failed, parameters.Message);
        }
    }
    public static class Extra
    {
        public static Error InvalidDeleteOperation(
            ErrorParameters.Extra.InvalidDeleteOperation? parameters = null)
        {
            if (parameters is null)
            {
                return Error.
                    Conflict(
                        Constants.ErrorsCode.DeleteIsInvalid, Constants.ErrorsGeneralMessage.DeleteIsInvalid);
            }
            
            return Error.
                Conflict(
                    Constants.ErrorsCode.DeleteIsInvalid,
                    $"{parameters.ObjectName} with {parameters.ObjectValue} cannot be deleted");
        }
        public static Error RoleIsInvalid(
            ErrorParameters.Extra.RoleIsInvalid? parameters = null)
        {
            if(parameters is null)
            {
                return Error.
                    Conflict(Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsExtraMessage.InvalidRole);
            }
            
            return Error.Conflict(
                    Constants.ErrorsCode.ValueIsInvalid, parameters.Message);
        }
        public static Error PositionIsInvalid(ErrorParameters.Extra.PositionIsInvalid? parameters = null)
        {
            if(parameters is null)
            {
                return Error.
                    NotFound(
                        Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsExtraMessage.InvalidPosition);
            }
            
            return Error.
                NotFound(
                    Constants.ErrorsCode.ValueIsInvalid, $"Position {parameters.Position} does not exist");
        }
        public static Error TokenIsInvalid()
        {
            return Error.Validation(
                Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsExtraMessage.InvalidToken);
        }
        public static Error TokenIsExpired()
        {
            return Error.
                Failure(Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsExtraMessage.ExpiredToken);
        }
        public static Error CredentialsIsInvalid()
        {
            return Error.Validation(
                Constants.ErrorsCode.ValueIsInvalid, Constants.ErrorsExtraMessage.InvalidCredentials);
        }
        public static Error AlreadyExists(ErrorParameters.Extra.ValueAlreadyExists? parameters = null)
        {
            if(parameters is null)
            {
                return Error.Conflict(
                    Constants.ErrorsCode.ValueAlreadyExists, Constants.ErrorsExtraMessage.ValueAlreadyExists);
            }
            
            return Error.Conflict(
                Constants.ErrorsCode.ValueAlreadyExists, parameters.Message);
        }
    }
}