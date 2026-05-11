namespace BetStream.Infrastucture.Services
{
    using BetStream.Domain.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var jwtKey = _config["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key configuration is required.");
            var jwtIssuer = _config["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer configuration is required.");
            var jwtAudience = _config["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience configuration is required.");
            var tokenDuration = _config["Jwt:DurationInMinutes"]
                ?? throw new InvalidOperationException("Jwt:DurationInMinutes configuration is required.");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    double.Parse(tokenDuration)
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
