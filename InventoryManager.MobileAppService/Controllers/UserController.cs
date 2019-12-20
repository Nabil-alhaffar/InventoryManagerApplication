using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.Models;
using InventoryManager.MobileAppService.Models;
using Microsoft.AspNetCore.Authorization;
namespace InventoryManager.MobileAppService.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<User>> List()
        {
            return UserRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser(int id)
        {
            User user = UserRepository.Get(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Create([FromBody]User user)
        {
            UserRepository.Add(user);
            return CreatedAtAction(nameof(GetUser), new { user.UserName }, user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] User user)
        {
            try
            {
                UserRepository.Update(user);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            User user = UserRepository.Remove(id);

            if (user == null)
                return NotFound();

            return Ok();
        }
    }
}
