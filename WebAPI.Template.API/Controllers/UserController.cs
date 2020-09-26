using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Template.API.Models.Request;
using WebAPI.Template.API.Models.Response;

namespace WebAPI.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<UserTokenResponse> Login(LoginRequest request)
        {
            var dados = new UserResponse() { UserId = 1, UserName = "Troca Sombra", Age = 30 };


            if (string.IsNullOrEmpty(request.User) || string.IsNullOrEmpty(request.Password))
                return BadRequest();

            if (request.User == "trocasombra@hotmail.com" && request.Password == "123")
                return Ok(BuildToken(dados));
            else
                return NotFound("Usuário ou senha inválidos");
        }


        private UserTokenResponse BuildToken(UserResponse userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName),
                new Claim("meuValor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UserTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}