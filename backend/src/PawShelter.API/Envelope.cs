using PawShelter.Domain.Shared;

namespace PawShelter.API;

public record Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
    }
    public object? Result { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) =>
        new(result, null);

    public static Envelope Error(Error error) =>
        new(null, error);
}
