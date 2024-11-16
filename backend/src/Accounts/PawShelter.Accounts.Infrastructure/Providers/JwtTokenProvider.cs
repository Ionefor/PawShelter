using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.Options;

namespace PawShelter.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _options;

    public JwtTokenProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        Claim[] claims = [ 
            new(CustomClaims.Sub, user.Id.ToString()),
            new(CustomClaims.Email, user.Email ?? "")];
        
        var jwtToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_options.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims);
        
        var tokenString =  new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return tokenString;
    }
}