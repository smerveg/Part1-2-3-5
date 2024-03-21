using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ProductContext _context;

        public TokenController(IConfiguration config,ProductContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromQuery]UserDTO userInfo)
        {
            if (userInfo.UserName!=null && userInfo.Password!=null)
            {
                var user = await GetUser(userInfo.UserName, userInfo.Password);

                if (user!=null)
                {
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]));

                    JwtSecurityToken jwt = new JwtSecurityToken(
                            issuer: _config["AppSettings:ValidIssuer"],
                            audience: _config["AppSettings:ValidAudience"],
                            claims: new List<Claim> {
                            new Claim("userName", user.UserName),
                            new Claim("password", user.Password),

                            },

                            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserDTO> GetUser(string userName,string password)
        {
            var result= await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            UserDTO userDTO = new UserDTO()
            {
                UserName = result.UserName,
                Password = result.Password
            };
            return userDTO;
        }
    }
}
