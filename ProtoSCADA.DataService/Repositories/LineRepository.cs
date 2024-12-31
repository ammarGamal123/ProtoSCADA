using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class LineRepository : GenericRepository<Line>, ILineRepository
{
    public LineRepository(ApplicationDbContext context) : base(context) { }

    public async Task<LineDto> GetLineByIdAsync(int lineId)
    {
        return await _dbSet
            .Include(l => l.Factory)
            .Include(l => l.Supervisor)
            .Where(l => l.ID == lineId)
            .Select(l => new LineDto
            {
                ID = l.ID,
                Name = l.Name,
                Description = l.Description,
                LineNumber = l.LineNumber,
                FactoryName = l.Factory.Name,
                LastMaintenanceDate = l.LastMaintenanceDate,
                IsActive = l.IsActive,
                SupervisorName = l.Supervisor.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<LineDto>> GetAllLinesAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(l => l.Factory)
            .Include(l => l.Supervisor)
            .Select(l => new LineDto
            {
                ID = l.ID,
                Name = l.Name,
                Description = l.Description,
                LineNumber = l.LineNumber,
                FactoryName = l.Factory.Name,
                LastMaintenanceDate = l.LastMaintenanceDate,
                IsActive = l.IsActive,
                SupervisorName = l.Supervisor.Name
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}