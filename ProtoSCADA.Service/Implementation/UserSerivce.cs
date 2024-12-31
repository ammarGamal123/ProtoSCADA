using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProtoSCADA.Service.Validation;

namespace ProtoSCADA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork = null)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // Add a new user
        public async Task<ProcessResult<bool>> AddUserAsync(User user)
        {
            if (user == null)
                return ProcessResult<bool>.Failure("User cannot be null.", false);

            try
            {
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("User added successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding user: {ex.Message}", false);
            }
        }

        // Delete a user by ID
        public async Task<ProcessResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return ProcessResult<bool>.Failure($"No user found with ID {id}.", false);

                await _unitOfWork.Users.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("User deleted successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting user: {ex.Message}", false);
            }
        }

        public async Task<ProcessResult<IEnumerable<User>>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                var validationResult = ValidatePagination.Validate(pageNumber, pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<User>>.Failure(validationResult.Message);
                }

                // Fetch all users from the repository
                var users = await _userRepository.GetAllAsync();

                // Apply pagination
                var paginatedUsers = users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Return success result with paginated data
                return ProcessResult<IEnumerable<User>>.Success("Users retrieved successfully.", paginatedUsers);
            }
            catch (Exception ex)
            {
                // Return failure with appropriate error message and an empty collection instead of null
                return ProcessResult<IEnumerable<User>>.Failure($"Error retrieving users: {ex.Message}", Enumerable.Empty<User>());
            }
        }

        // Retrieve a user by ID
        public async Task<ProcessResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return ProcessResult<User>.Failure($"User with ID {id} not found.", null);

                return ProcessResult<User>.Success("User retrieved successfully.", user);
            }
            catch (Exception ex)
            {
                return ProcessResult<User>.Failure($"Error retrieving user: {ex.Message}", null);
            }
        }

        // Update a user's details
        public async Task<ProcessResult<bool>> UpdateUserAsync(User user)
        {
            if (user == null)
                return ProcessResult<bool>.Failure("User cannot be null.", false);

            try
            {
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("User updated successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating user: {ex.Message}", false);
            }
        }

        // Retrieve all users as DTOs (if needed)
        public async Task<ProcessResult<IEnumerable<UserDto>>> GetAllUsersDtoAsync(int pageNumber, int pageSize)
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync(pageNumber, pageSize);
                return ProcessResult<IEnumerable<UserDto>>.Success("Users retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<UserDto>>.Failure($"Error retrieving users: {ex.Message}", Enumerable.Empty<UserDto>());
            }
        }

        // Retrieve a user by ID as DTO (if needed)
        public async Task<ProcessResult<UserDto>> GetUserDtoByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                    return ProcessResult<UserDto>.Failure($"User with ID {id} not found.", null);

                return ProcessResult<UserDto>.Success("User retrieved successfully.", user);
            }
            catch (Exception ex)
            {
                return ProcessResult<UserDto>.Failure($"Error retrieving user: {ex.Message}", null);
            }
        }
    }
}