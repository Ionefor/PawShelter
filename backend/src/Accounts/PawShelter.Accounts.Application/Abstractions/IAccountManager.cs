using PawShelter.Accounts.Domain.Accounts;

namespace PawShelter.Accounts.Application.Abstractions;

public interface IAccountManager
{
    public Task CreateAdminAccount(AdminAccount adminAccount);

    public Task CreateParticipantAccount(ParticipantAccount participantAccount);

    public Task<bool> AdminAccountExists(CancellationToken cancellationToken = default);
}