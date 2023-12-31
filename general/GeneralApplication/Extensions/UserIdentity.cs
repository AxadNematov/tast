using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GeneralDomain;
using GeneralDomain.EntityModels;
using Microsoft.IdentityModel.Tokens;

namespace GeneralApplication.Extensions;

public static class UserIdentity
{
    public static ClaimsIdentity GetIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("userName", user.UserName),
            
        };
        
        ClaimsIdentity claimsIdentity =

            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }

    public static string AccessToken(User user)
    {
        var identity = GetIdentity(user);
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return encodedJwt ?? "";
    }

    public static string RefreshToken(User user)
    {
        user.RefreshToken = Guid.NewGuid().ToString();
        
        return user.RefreshToken;
    }
}