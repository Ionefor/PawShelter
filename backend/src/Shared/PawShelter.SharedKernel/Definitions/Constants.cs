namespace PawShelter.SharedKernel.Definitions;

public static class Constants
{
   public static class Shared
   {
      public const int MaxLowTextLength = 20;
      public const int MaxMediumTextLength = 100;
      public const int MaxLargeTextLength = 2000;
      
      public const int MaxDegreeOfParallelism = 10;
      public const string BucketNamePhotos = "Photos";
      public const string Database = "Database";
      
      public const string ConfigurationsWrite = "Configurations.Write";
      public const string ConfigurationsRead = "Configurations.Read";
   }
   public static class Volunteers 
   {
      public const int MaxYearExperience = 100;
      public const int MinYearExperience = 0;

      public const int MinYearBirthday = 1950;
      
      public const int MinPhysicalValue = 0;
      public const int MaxPhysicalValue = 400;
   }
   public static class Accounts
   {
      public const string AccountsPath = "etc/accounts.json";
   }
   public static class ErrorsCode
   {
      public const string InternalServer = "server.is.internal";
      public const string Failed = "failed.operation";
      public const string NotFound = "value.not.found";
      public const string ValueIsRequired = "value.is.required";
      public const string ValueIsInvalid = "value.is.invalid";
      public const string DeleteIsInvalid = "cannot.delete.operation";
      public const string ValueAlreadyExists = "value.already.exists";
   }
   
   public static class ErrorsGeneralMessage
   {
      public const string InternalServer = "Server is internal";
      public const string Failed = "Failed to do operation";
      public const string NotFound = "Value not found";
      public const string ValueIsRequired = "Value cannot be null or empty";
      public const string ValueIsInvalid = "Value is invalid";
      public const string DeleteIsInvalid = "Cannot be deleted";
   }
   
   public static class ErrorsExtraMessage
   {
      public const string ValueAlreadyExists = "Value already exists";
      public const string InvalidPosition = "Position does not exist";
      public const string InvalidToken = "Token is invalid";
      public const string ExpiredToken = "Token is expired";
      public const string InvalidRole = "Role is invalid";
      public const string InvalidCredentials = "Your credentials is invalid";
   }
}