using FoodDelivery.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        #region Constructor
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion Constructor

        #region Auth
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // get user detail from database
            var user = "User object";

            if (user != null)
            {
                var token = GetToken(model.Username, model.Password);
                return Ok(token);
            }
            return Unauthorized();
        }
        #endregion Auth

        #region Public
        [HttpPost]
        [Authorize]
        [Route("AddItemToCart")]
        public void AddToCart([FromBody] string value)
        {
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveItemFromCart")]
        public void RemoveFromCart([FromBody] string value)
        {
        }

        [HttpPost]
        [Authorize]
        [Route("AddUserDeliveryDetail")]
        public void AddUserDeliveryDetail([FromBody] string value)
        {
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserCartDetail/{id}")]
        public string GetCartDetail(int id)
        {
            return "value";
        }

        
        #endregion Public

        #region JWT Token
        private string GetToken(string userName, string password)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Email, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
        #endregion JWT Token
    }
}
