using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class MachineRepository : GenericRepository<Machine>, IMachineRepository
{
    public MachineRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Machine> GetMachineByIdAsync(int machineId)
    {
        return await _dbSet.Include(m => m.Factory)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(m => m.ID == machineId);
    }

    public async Task<IEnumerable<MachineDto>> GetAllMachinesAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(m => m.Factory)
            .Select(m => new MachineDto
            {
                MachineID = m.ID,
                MachineType = m.Type,
                Status = m.Status,
                LastMaintance = m.LastMaintenance,
                FactorName = m.Factory.Name
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}