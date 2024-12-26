using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;

namespace ProtoSCADA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet(Name ="Get All Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users.", Details = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserByID (int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the user.", Details = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser (int id)
        {
            try
            {
                var userToDelete = _userService.DeleteUserAsync(id);
                if (userToDelete == null)
                {
                    return NotFound(new { Mewssage = $"User with ID {id} not found." });
                }
                return Ok($"User with {id}, Deleted Successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while trying to delete the user.", Details = ex.Message });
            }
        }
    }
}
