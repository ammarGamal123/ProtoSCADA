using ProtoSCADA.Data.Repositories;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
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

        public async Task AddUserAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error adding user: {ex.Message}", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _unitOfWork.Users.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting user with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _unitOfWork.Users.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving all users: {ex.Message}", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving user by ID {id}: {ex.Message}", ex);
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating user: {ex.Message}", ex);
            }
        }
    }
}
