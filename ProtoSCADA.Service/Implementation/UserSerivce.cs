using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
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
                var user = await _unitOfWork.Users.GetByIdAsync(id);
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

        // Retrieve all users
        public async Task<ProcessResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                return ProcessResult<IEnumerable<User>>.Success("Users retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<User>>.Failure($"Error retrieving users: {ex.Message}", null);
            }
        }

        // Retrieve a user by ID
        public async Task<ProcessResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
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
    }
}
