using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync(int pageNumber, int pageSize);
    Task<EventDto> GetEventByIdAsync(int eventId);
}