using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(AppDbContext context) : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.UserName) ||string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Enter your name and password");
            }

            try
            {
                var exists = context.Users.Any(n => n.UserName == user.UserName && n.Password == user.Password);
                if (exists)
                {
                    return Ok(CreateToken(user));
                }

                return BadRequest("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("register")]
        [HttpPost]
        public ActionResult Register([FromBody] User user)
        {
            try
            {
                var exists = context.Users.Any(n => n.UserName == user.UserName );
                if (exists)
                {
                    return BadRequest("user alredy exists");
                }
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(CreateToken(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private JwtPackage CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.UserName)
            });

            const string secretKey = "your secret key goes here";
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));

            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(
                subject: claims,
                signingCredentials: signinCredentials
            );

            var tokenString = tokenHandler.WriteToken(token);

            return new JwtPackage()
            {
                UserName = user.UserName,
                Token = tokenString
            };
        }
    }
}

public class JwtPackage
{
    public string Token { get; set; }
    public string UserName { get; set; }
}