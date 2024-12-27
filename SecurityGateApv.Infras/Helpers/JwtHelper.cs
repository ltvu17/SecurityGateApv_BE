using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Interfaces.Jwt;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Helpers
{
    public class JwtHelper : IJwt
    {
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DecodeJwt(string header)
        {
            var handler = new JwtSecurityTokenHandler();
            header = header.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(header);
            var tokenS = handler.ReadToken(header) as JwtSecurityToken;
            var role = tokenS.Claims.First(claim => claim.Type == "role").Value;
            return role;
        }
        public int DecodeJwtUserId(string header)
        {
            var handler = new JwtSecurityTokenHandler();
            header = header.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(header);
            var tokenS = handler.ReadToken(header) as JwtSecurityToken;
            var role = tokenS.Claims.First(claim => claim.Type == "userId").Value;
            return int.Parse(role);
        }
        public UserAuthorDTO DecodeAuthorJwt(string header)
        {
            var handler = new JwtSecurityTokenHandler();
            header = header.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(header);
            var tokenS = handler.ReadToken(header) as JwtSecurityToken;
            var role = tokenS.Claims.First(claim => claim.Type == "role").Value;
            var userId = tokenS.Claims.First(claim => claim.Type == "userId").Value;
            var departmentId = tokenS.Claims.First(claim => claim.Type == "departmentId").Value;
            return new UserAuthorDTO { 
                UserId = int.Parse(userId),
                DepartmentId = int.Parse(departmentId),
                Role = role
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["JWT:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("role", user.Role.RoleName.ToString()),
                new Claim("departmentId", user.DepartmentId.ToString()),
                new Claim("userId", user.UserId.ToString()),

            };

            var token = new JwtSecurityToken(
                _configuration["JWT:Issure"],
                _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}
