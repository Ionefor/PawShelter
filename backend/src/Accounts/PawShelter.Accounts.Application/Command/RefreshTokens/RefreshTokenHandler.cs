using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Contracts.Responses;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Models;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Accounts.Application.Command.RefreshTokens;

public class RefreshTokenHandler : ICommandHandler<LoginResponse, RefreshTokenCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    public RefreshTokenHandler(
        IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork)
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
            return Errors.Extra.TokenIsExpired().ToErrorList();
        }
        
        var userClaims = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
        if(userClaims.IsFailure)
            return userClaims.Error.ToErrorList();
        
        var userIdString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
        
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return Errors.General.
                ValueIsRequired(
                    new ErrorParameters.General.ValueIsRequired(nameof(CustomClaims.Id))).ToErrorList();
        }
        
        var userJtiString = userClaims.Value.FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
        
        if (!Guid.TryParse(userJtiString, out Guid userJti))
        {
            return Errors.General.
                ValueIsRequired(
                    new ErrorParameters.General.ValueIsRequired(nameof(CustomClaims.Jti))).ToErrorList();
        }
        
        if (oldRefreshSession.Value.UserId != userId)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(
                    nameof(oldRefreshSession.Value.UserId))).ToErrorList();
        }
        
        if (oldRefreshSession.Value.Jti != userJti)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(
                    nameof(oldRefreshSession.Value.Jti))).ToErrorList();
        }

        _refreshSessionManager.Delete(oldRefreshSession.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = _tokenProvider.
            GenerateAccessToken(oldRefreshSession.Value.User, cancellationToken);
        
        var refreshToken = _tokenProvider.
            GenerateRefreshToken(oldRefreshSession.Value.User, accessToken.Result.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.Result.AccessToken, refreshToken.Result);
    }
}