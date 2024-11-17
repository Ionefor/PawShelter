using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.Accounts.Infrastructure.Options;
using PawShelter.Accounts.Infrastructure.Seading;
using PawShelter.Core.Models;

namespace PawShelter.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOtions;
    private readonly AccountDbContext _accountsDbContext;
    private readonly PermissionManager _permissionsManager;

    public JwtTokenProvider(
        IOptions<JwtOptions> options,
        AccountDbContext accountsDbContext,
        PermissionManager permissionsManager)
    {
        _jwtOtions = options.Value;
        _accountsDbContext = accountsDbContext;
        _permissionsManager = permissionsManager;
    }
    public async Task<string> GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOtions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles.Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

        var permissions = await _permissionsManager.GetUserPermissions(user.Id, cancellationToken);
        var permissionClaims = permissions.Select(p => new Claim(CustomClaims.Permission, p));
        
        Claim[] claims = [
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? ""),
        ];

        claims = claims.Concat(roleClaims).Concat(permissionClaims).ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOtions.Issuer,
            audience: _jwtOtions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOtions.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims);

        var jwtStringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return jwtStringToken;
    }
}