using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Where(u => u.ID == userId)
            .Select(u => new UserDto
            {
                ID = u.ID,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.RoleName // Assuming Role has a Name property
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Select(u => new UserDto
            {
                ID = u.ID,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.RoleName
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}
