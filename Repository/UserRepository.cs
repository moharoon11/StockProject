using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace api.Repository 
{


    public class UserRepository(ApplicationDbContext context, IConfiguration configuration) : IUserRepository {
   
    
         public async Task<User> Register(UserDto dto) {

            if(await context.Users.AnyAsync(u => u.Username == dto.Username)) {
                return null;
            }

           var user = new User();
           user.Username = dto.Username;
           user.HashPassword = new PasswordHasher<User>().HashPassword(user, dto.Password);
           user.Role = dto.Role;
         
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
         }

         public async Task<string> Login(UserDto dto) {

            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if(user == null) {
                return null;
            }

            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.HashPassword, dto.Password) == PasswordVerificationResult.Failed) {
                return null;
            }

            var token = CreateToken(user);
            return token;
         }

         private string CreateToken(User user) {

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("AppSettings:Token")!
            ));

            var cred = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512
            );

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
         }

      
    }
}