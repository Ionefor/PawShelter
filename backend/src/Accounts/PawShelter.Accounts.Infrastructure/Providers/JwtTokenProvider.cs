using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Application.Models;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.Authorization;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.Accounts.Infrastructure.Options;
using PawShelter.Accounts.Infrastructure.Seading;
using PawShelter.Core.Models;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly AccountsWriteDbContext _accountsWritesDbContext;
    private readonly PermissionManager _permissionManager;

    public JwtTokenProvider(
        IOptions<JwtOptions> options,
        AccountsWriteDbContext accountsWritesDbContext,
        PermissionManager permissionManager)
    {
        _jwtOptions = options.Value;
        _accountsWritesDbContext = accountsWritesDbContext;
        _permissionManager = permissionManager;
    }
   public async Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        
        var permissions = await _permissionManager.GetUserPermissions(user.Id, cancellationToken);
        var permissionClaims = permissions.Select(p => new Claim(CustomClaims.Permission, p));
        
        var jti = Guid.NewGuid();

        Claim[] claims =
        [
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? ""),
            new Claim(CustomClaims.Jti, jti.ToString())
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();
        
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims);
        
        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokenResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user,
        Guid accessTokenJti,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(60),
            Jti = accessTokenJti,
            RefreshToken = Guid.NewGuid()
        };

        _accountsWritesDbContext.RefreshSessions.Add(refreshSession);
        await _accountsWritesDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(
        string jwtToken, 
        CancellationToken cancellationToken = default)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var validationParameters = TokenValidationParametersFactory.CreateWithoutLifeTime(_jwtOptions);

        var validationResult =  await jwtHandler.ValidateTokenAsync(jwtToken, validationParameters);

        if (!validationResult.IsValid)
            return Errors.Extra.TokenIsInvalid();

        return validationResult.ClaimsIdentity.Claims.ToList();
    }
}