using CosmosDb.DAL.Abstraction;
using CosmosDb.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDb.DAL.Implementation
{
    public class Auth:IAuth
    {
        private readonly AppSettings _appSettings;

        public Auth(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }
        private List<AuthUser> users = new List<AuthUser>()
        {
            new AuthUser{UserId="1",FirstName="Ruksana",LastName="khanum",UserName="ruksana",Password="khan"}
        };

        public AuthUser Authenticate(string userName, string Password)
        {
            var user = users.SingleOrDefault(x => x.UserName == userName && x.Password == Password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name,user.UserId.ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Version,"V3.1")

                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }
    }
}
