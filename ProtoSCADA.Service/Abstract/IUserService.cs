using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IUserService
    {
        Task<ProcessResult<User>> GetUserByIdAsync(int id);
        Task<ProcessResult<IEnumerable<User>>> GetAllUsersAsync(int pageNumber , int pageSize);
        Task<ProcessResult<bool>> AddUserAsync(User user);
        Task<ProcessResult<bool>> UpdateUserAsync(User user);
        Task<ProcessResult<bool>> DeleteUserAsync(int id);
    }
}
