using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MangaOnline.ModelCore;
using Microsoft.IdentityModel.Tokens;

namespace MangaOnline.Extensions;

public static class AuthenticationPage
{
    public static string WriteToken(string fullName, string email, string role)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var claims = new[]
        {
            new Claim("name", fullName),
            new Claim("email", email),
            new Claim("role", role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: config["jwt:Issuer"],
            audience: config["jwt:Issuer"],
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["jwt:key"])),
                SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static JwtUser? ReadToken(string tokenString)
    {
        try
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["jwt:key"])),
                ValidIssuer = config["jwt:Issuer"],
                ValidAudience = config["jwt:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new SecurityTokenException("Invalid token");
            }

            var userToken = new JwtUser
            {
                email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")!.Value,
                name = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")!.Value,
                role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")!.Value
            };
            return userToken;
        }
        catch
        {
            return null;
        }
    }
}