using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Servies;

public class TokenServies(IConfiguration config) : ITokenServies
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"]?? throw new Exception("Cannot access tokenkey from appsetting");
        if(tokenKey.Length < 64) throw new Exception("Your tokenkey needs to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        { 
            new (ClaimTypes.NameIdentifier, user.UserName)
        };
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = cred
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

internal record struct NewStruct(string NameIdentifier, string UserName)
{
    public static implicit operator (string NameIdentifier, string UserName)(NewStruct value)
    {
        return (value.NameIdentifier, value.UserName);
    }

    public static implicit operator NewStruct((string NameIdentifier, string UserName) value)
    {
        return new NewStruct(value.NameIdentifier, value.UserName);
    }
}