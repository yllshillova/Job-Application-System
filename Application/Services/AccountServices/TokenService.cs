using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.AccountServices;

public class TokenService
{
    public string CreateToken(User user)
    {
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),


        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Iww4kxaMt6GzDUj4vbqnJPXgIadhgeNMvBr55NNjga7HBvDGzajJ8YbN8ecMeCZe"));
        var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
