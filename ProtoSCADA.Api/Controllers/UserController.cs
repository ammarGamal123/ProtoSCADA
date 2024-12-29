using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtoSCADA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet(Name = "Get All Users")]
        public async Task<ActionResult<ProcessResult<IEnumerable<UserDto>>>> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();
                if (result.IsSuccess)
                {
                    // Convert to DTO before returning
                    var userDtos = result.Data.Select(user => new UserDto
                    {
                        ID = user.ID,
                        Name = user.Name,
                        Email = user.Email,
                        Role = user.Role,
                        CreatedAt = user.CreatedAt
                    });

                    return Ok(ProcessResult<IEnumerable<UserDto>>.Success(result.Message, userDtos));
                }
                return StatusCode(500, new { Message = "An error occurred while retrieving users." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A user details.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUserByID(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                if (result.IsSuccess)
                {
                    // Convert to DTO before returning
                    var userDto = new UserDto
                    {
                        ID = result.Data.ID,
                        Name = result.Data.Name,
                        Email = result.Data.Email,
                        Role = result.Data.Role,
                        CreatedAt = result.Data.CreatedAt
                    };
                    return Ok(ProcessResult<UserDto>.Success(result.Message, userDto));
                }

                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the user.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The user ID to delete.</param>
        /// <returns>Status of deletion.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (result.IsSuccess)
                {
                    return Ok(new { Message = $"User with ID {id} deleted successfully." });
                }

                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while trying to delete the user.", Details = ex.Message });
            }
        }
    }
}
