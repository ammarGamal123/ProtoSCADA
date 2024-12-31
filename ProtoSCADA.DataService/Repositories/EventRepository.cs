using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context) : base(context) { }

    public async Task<EventDto> GetEventByIdAsync(int eventId)
    {
        return await _dbSet
            .Include(e => e.User)
            .Include(e => e.Machine)
            .Include(e => e.Line)
            .Include(e => e.Factory)
            .Where(e => e.ID == eventId)
            .Select(e => new EventDto
            {
                ID = e.ID,
                Type = e.Type,
                Description = e.Description,
                UserID = e.UserID,
                MachineID = e.MachineID,
                LineID = e.LineID,
                FactoryID = e.FactoryID
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(e => e.User)
            .Include(e => e.Machine)
            .Include(e => e.Line)
            .Include(e => e.Factory)
            .Select(e => new EventDto
            {
                ID = e.ID,
                Type = e.Type,
                Description = e.Description,
                UserID = e.UserID,
                MachineID = e.MachineID,
                LineID = e.LineID,
                FactoryID = e.FactoryID
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}