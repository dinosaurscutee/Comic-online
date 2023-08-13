using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MangaOnline.Extensions;

public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    private readonly JwtHeader _jwtHeader;
    private readonly SigningCredentials _signingCredentials;

    public JwtTokenHandler(SecurityKey securityKey)
    {
        _jwtHeader = new JwtHeader(new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    public string WriteToken(string fullName, string email, string role)
    {
        var payload = new JwtPayload
        {
            { "role", role },
            { "name", fullName },
            { "email", email },
            { "exp", DateTime.UtcNow.AddDays(30) },
            { "iat", DateTime.UtcNow }
        };
        var token = new JwtSecurityToken(_jwtHeader, payload);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    public JwtSecurityToken ReadToken(string token)
    {
        return _jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
    }
}