using FoodDelivery.Model;
using FoodDelivery.Services;
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
        public IActionResult Login([FromBody] LoginModel model)
        {
            // get user detail from database
            dbServices dbMethods = new dbServices();
                
            var user = dbMethods.getUser(model);

            if (user != null)
            {
                var token = GetToken(model.Username, model.Password);

                user.Token = token;
                return Ok(user);
            }
            return Unauthorized();
        }
        #endregion Auth

        #region Public
        [HttpPost]
        [Authorize]
        [Route("AddItemToCart")]
        public IActionResult AddToCart([FromBody] CartModel model, string Token)
        {
            // Auth. the Token

            if(true/*isValidToken()*/)
            {
                dbServices dbMethods = new dbServices();

                bool isIteamAdded = dbMethods.addCartItem(model);

                if (isIteamAdded)
                    return Ok(isIteamAdded);
                else
                    return Ok(isIteamAdded);
            }
            return Unauthorized();
            
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveItemFromCart")]
        public IActionResult RemoveFromCart([FromBody] int userID,int ItemId, string Token)
        {
            // Auth. the Token

            if (true/*isValidToken()*/)
            {
                dbServices dbMethods = new dbServices();

                bool isIteamRemoved = dbMethods.removeCartIteam(userID,ItemId);

                if (isIteamRemoved)
                    return Ok(isIteamRemoved);
                else
                    return Ok(isIteamRemoved);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        [Route("AddUserDeliveryDetail")]
        public IActionResult AddUserDeliveryDetail([FromBody] AddressModel model, string Token)
        {
            // Auth. the Token

            if (true/*isValidToken()*/)
            {
                dbServices dbMethods = new dbServices();

                bool isAdded = dbMethods.deliveryDetails(model);

                if (isAdded)
                    return Ok(isAdded);
                else
                    return Ok(isAdded);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserCartDetail/{id}")]
        public IActionResult GetCartDetail(int UserId, string Token)
        {
            // Auth. the Token

            if (true/*isValidToken()*/)
            {
                dbServices dbMethods = new dbServices();

                var cartItemList = dbMethods.getCartDetail(UserId);

                CartRespModel cartDetails = new CartRespModel();

                //Add loop for Food Item and get Price

                cartDetails.Items = cartItemList;
                cartDetails.TotalItem = cartItemList.Count;
                cartDetails.TotalPrice = 0;

                return Ok(cartItemList);
            }
            return Unauthorized();
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
