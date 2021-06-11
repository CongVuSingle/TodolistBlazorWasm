using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.API.Entities;
using TodolistBlazor.Models.LoginModels;

namespace TodolistBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        //tạo 1 method để nhân username và password nhập từ client, xử lý việc đăng nhập nếu dứng thì trả về 1 token nếu sai trả về 1 error
        private readonly IConfiguration _configuration; // tao token lay 4 thong tin tu config "JwtIssuer", "JwtAudience", "JwtSecurityKey", "JwtExpiryInDays"
        private readonly SignInManager<User> _signInManager;

        public LoginController(IConfiguration configuration, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName), // claim là thông tin đính kèm ở trong token
            }; 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // dùng thuật toán sha256 để mã hóa
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
