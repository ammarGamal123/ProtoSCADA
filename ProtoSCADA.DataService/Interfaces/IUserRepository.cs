using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize);
    Task<UserDto> GetUserByIdAsync(int userId);
}