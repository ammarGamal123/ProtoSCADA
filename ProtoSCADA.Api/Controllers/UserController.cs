using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtoSCADA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of user DTOs wrapped in a ProcessResult.</returns>
        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<UserDto>>>> GetAllUsers(int pageNumber = 1 , int pageSize = 10)
        {
            _logger.LogInformation("Fetching all users.");

            try
            {
                var result = await _userService.GetAllUsersAsync(pageNumber , pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No users found.");
                    return NotFound(ProcessResult<IEnumerable<UserDto>>.Failure("No users found."));
                }

                var userDtos = result.Data.Select(MapToDto).ToList();
                return Ok(ProcessResult<IEnumerable<UserDto>>.Success(userDtos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving users.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<UserDto>>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A single user DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<UserDto>>> GetUserById(int id)
        {
            _logger.LogInformation($"Fetching user with ID: {id}");

            try
            {
                var result = await _userService.GetUserByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(ProcessResult<UserDto>.Failure("User not found."));
                }

                var userDto = MapToDto(result.Data);
                return Ok(ProcessResult<UserDto>.Success(userDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving user with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<UserDto>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");

            try
            {
                var result = await _userService.DeleteUserAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"User with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("User not found."));
                }

                _logger.LogInformation($"User with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting user with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Maps a user entity to its DTO representation.
        /// </summary>
        /// <param name="user">The user entity.</param>
        /// <returns>The mapped UserDto.</returns>
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role?.RoleName, // Safely access Role properties
                CreatedAt = user.CreatedAt
            };
        }
    }
}