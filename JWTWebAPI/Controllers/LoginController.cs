using JWTWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using JWTWebAPI;
using JWTWebAPI.Data;

namespace JWTWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly ConexionDBContext _context;

        public LoginController(ConexionDBContext conexion)
        {
            _context = conexion;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            var ListaUsuarios =  _context.Login.ToList<Login>();

            if (login is null)
            {
                return BadRequest();
            }
            foreach (var user in ListaUsuarios)
            {
                if (login.Usuario == user.Usuario && login.Contrasenia == user.Contrasenia)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfiguracionManager.AppSetting["JWT:SigningKey"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(issuer: ConfiguracionManager.AppSetting["JWT:Issuer"], audience: ConfiguracionManager.AppSetting["JWT:Audience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(7200), signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new
                    {
                        Token = tokenString
                    });
                }
            }
            return Unauthorized();
        }
    }
}
