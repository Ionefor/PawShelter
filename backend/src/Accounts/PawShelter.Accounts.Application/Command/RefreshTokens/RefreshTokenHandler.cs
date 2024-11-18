using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Contracts.Responses;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Models;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Application.Command.RefreshTokens;

public class RefreshTokenHandler : ICommandHandler<LoginResponse, RefreshTokenCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenHandler(
        IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        [FromKeyedServices("Accounts")]IUnitOfWork unitOfWork)
    {
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        var oldRefreshSession = await _refreshSessionManager.GetByRefreshToken(command.RefreshToken);
        if(oldRefreshSession.IsFailure)
            return oldRefreshSession.Error.ToErrorList();

        if (oldRefreshSession.Value.ExpiresIn < DateTime.Now)
        {
            //реф ошибки
            return Error.Deserialize(
                "token.is.expires||refreshToken is expires||Failure").ToErrorList();
        }
        
        var userClaims = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
        if(userClaims.IsFailure)
            return userClaims.Error.ToErrorList();
        
        var userIdString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            //реф ошибки
            return Error.Failure("claim.userId", "claim userId is null").ToErrorList();
        }
        
        var userJtiString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
        if (!Guid.TryParse(userJtiString, out Guid userJti))
        {
            //реф ошибки
            return Error.Failure("claim.userId", "claim userId is null").ToErrorList();
        }
        
        if (oldRefreshSession.Value.UserId != userId)
        {
            //реф ошибки
            return Errors.General.ValueIsInvalid().ToErrorList();
        }
        
        if (oldRefreshSession.Value.Jti != userJti)
        {
            //реф ошибки
            return Errors.General.ValueIsInvalid().ToErrorList();
        }

        _refreshSessionManager.Delete(oldRefreshSession.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = _tokenProvider.GenerateAccessToken(
            oldRefreshSession.Value.User, cancellationToken);
        
        var refreshToken = _tokenProvider.GenerateRefreshToken(
            oldRefreshSession.Value.User, accessToken.Result.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.Result.AccessToken, refreshToken.Result);
    }
}