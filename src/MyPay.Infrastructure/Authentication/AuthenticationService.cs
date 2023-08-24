using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Domain.Customers;
using MyPay.Domain.Shopkeepers;
using MyPay.Infrastructure.Policies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyPay.Infrastructure.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationOptions _authenticationOptions;

    public AuthenticationService(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
    }

    public string GenerateJwtToken(Customer customer)
    {
        var claims = new[]
        {
            new Claim(IdentityData.UserIdName, customer.Id.Value.ToString()),
            new Claim(IdentityData.CustomerPolicyKey, IdentityData.CustomerPolicyValue)
        };
        return GenerateTokenWithClaims(claims);
    }

    public string GenerateJwtToken(Shopkeeper shopkeeper)
    {
        var claims = new[]
        {
            new Claim(IdentityData.UserIdName, shopkeeper.Id.Value.ToString()),
            new Claim(IdentityData.ShopkeeperPolicyKey, IdentityData.ShopkeeperPolicyValue)
        };
        return GenerateTokenWithClaims(claims);
    }

    private string GenerateTokenWithClaims(Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _authenticationOptions.Issuer,
            audience: _authenticationOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_authenticationOptions.ExpireInMinutes),
            claims: claims,
            signingCredentials: creds
        );

        return tokenHandler.WriteToken(token);
    }
}
