namespace PawShelter.Accounts.Domain.Accounts;

public class ParticipantAccount
{
    public static string Participant = nameof(Participant);
    private ParticipantAccount() {}
    public static ParticipantAccount Create(User user)
    {
        return new ParticipantAccount
        {
            Id = Guid.NewGuid(),
            User = user
        };
    }
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}