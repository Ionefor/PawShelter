namespace PawShelter.Accounts.Domain.Accounts;

public class AdminAccount
{
    public static string Admin = nameof(Admin);
    private AdminAccount() {}

    public static AdminAccount Create(User user)
    {
        return new AdminAccount
        {
            Id = Guid.NewGuid(),
            User = user,
        };
    }
    
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
}