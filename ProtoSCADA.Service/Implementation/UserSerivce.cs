using ProtoSCADA.DataService.Repositories;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Implementation
{
    public class UserSerivce : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
