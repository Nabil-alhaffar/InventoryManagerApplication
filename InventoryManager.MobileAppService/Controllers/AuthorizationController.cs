using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;
//using System.Security.Claims;
//using Newtonsoft.Json;
//using System.IdentityModel.Tokens.Jwt;
////using Microsoft.IdentityModel.Tokens;

//using System;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
//using System.Security.Claims;
//using System.Web.Http;
//using Microsoft.Azure.Mobile.Server.Login;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IUserRepository UserRepository;

        public AuthorizationController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticationModel model)
        {
            var user = UserRepository.Authenticate(model.Username, model.Password, model.StoreId);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles  = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = UserRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(UserType.Admin.ToString()))
                return Forbid();

            var user = UserRepository.Get(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        //    private SqlDbContext dbContext;
        //    private string signingKey, audience, issuer;


        //    public AuthorizationController(SqlDbContext _db)
        //    {
        //        dbContext = _db;
        //        signingKey = Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
        //        var website = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
        //        audience = $"https://{website}/";
        //        issuer = $"https://{website}/";
        //    }
        //    [HttpPost]
        //    public IHttpActionResult Post([FromBody] User user)
        //    {
        //        if (user == null || user.UserName == null || user.Password == null ||
        //            user.UserName.Length == 0 || user.Password.Length == 0)
        //        {
        //            return BadRequest();
        //        }

        //        if (!IsValidUser(user))
        //        {
        //            return Unauthorized();
        //        }
        //        return Ok();

        //        var claims = new Claim[]
        //        {
        //                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
        //        };

        //        JwtSecurityToken token = Microsoft.Azure.Mobile.Server.Login.AppServiceLoginHandler.CreateToken(
        //                claims, signingKey, audience, issuer, TimeSpan.FromDays(30));
        //        return Ok(new LoginResult()
        //        {
        //            AuthenticationToken = token.RawData,
        //            User = new LoginResultUser { UserId = user.UserName }
        //        });
        //    }
        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            dbContext.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

        //    private bool IsValidUser(User user)
        //        {
        //            return dbContext.Users.Count(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password)) > 0;
        //        }
        //        public class LoginResult
        //    {
        //        [JsonProperty(PropertyName = "authenticationToken")]
        //        public string AuthenticationToken { get; set; }

        //        //[JsonProperty(PropertyName = "user")]
        //        public LoginResultUser User { get; set; }
        //    }

        //    public class LoginResultUser
        //    {
        //        [JsonProperty(PropertyName = "userId")]
        //        public string UserId { get; set; }
        //    }
    }


    }

