using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class AlertRepository : GenericRepository<Alert>, IAlertRepository
{
    public AlertRepository(ApplicationDbContext context) : base(context) { }

    public async Task<AlertDto> GetAlertByIdAsync(int alertId)
    {
        return await _dbSet
            .Include(a => a.Machine)
            .Include(a => a.Line)
            .Include(a => a.Factory)
            .Where(a => a.ID == alertId)
            .Select(a => new AlertDto
            {
                ID = a.ID,
                ThresholdValue = a.ThresholdValue,
                Condition = a.Condition.ToString(),
                MachineID = a.MachineID,
                LineID = a.LineID,
                FactoryID = a.FactoryID
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AlertDto>> GetAllAlertsAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(a => a.Machine)
            .Include(a => a.Line)
            .Include(a => a.Factory)
            .Select(a => new AlertDto
            {
                ID = a.ID,
                ThresholdValue = a.ThresholdValue,
                Condition = a.Condition.ToString(),
                MachineID = a.MachineID,
                LineID = a.LineID,
                FactoryID = a.FactoryID
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}