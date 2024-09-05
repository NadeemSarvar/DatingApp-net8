using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController(DataContext _context, ITokenServies _token) : BaseApiController
    {
       
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if(await IsExists(registerDto.UserName)) return BadRequest("User already exists.");
            
            using var hmc = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmc.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
          
            return new UserDto
            {
                Username = user.UserName,
                Token = _token.CreateToken(user)
            };
        }
       [HttpPost("login")]
       public async Task<ActionResult<UserDto>> Login(LoginDto login)
       {
            var users = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName.ToLower());
            if(users == null) return Unauthorized("User not found.");

            using var hmac = new HMACSHA512(users.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != users.PasswordHash[i]) return Unauthorized("Invalid Password");
               
            }
             return new UserDto
             {
                Username = users.UserName,
                Token = _token.CreateToken(users)
             };
        }

        private async Task<bool> IsExists(string UserName)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == UserName.ToLower());
        }
    }
}
