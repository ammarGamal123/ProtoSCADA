using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class FactoryRepository : GenericRepository<Factory>, IFactoryRepository
{
    public FactoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<FactoryDto> GetFactoryByIdAsync(int factoryId)
    {
        return await _dbSet
            .Where(f => f.ID == factoryId)
            .Select(f => new FactoryDto
            {
                ID = f.ID,
                Name = f.Name,
                Location = f.Location
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FactoryDto>> GetAllFactoriesAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Select(f => new FactoryDto
            {
                ID = f.ID,
                Name = f.Name,
                Location = f.Location
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}