using FluentValidation.Results;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Core.Extensions;

public static class ValidationExtension
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(errorMessage)
            select Errors.General.ValueIsInvalid(
                new ErrorParameters.General.ValueIsInvalid(nameof(validationError.PropertyName)));

        return errors.ToList();
    }
}