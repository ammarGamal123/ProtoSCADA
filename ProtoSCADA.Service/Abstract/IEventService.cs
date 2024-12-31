using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IEventService
    {
        Task<ProcessResult<Event>> GetEventByIdAsync(int id);
        Task<ProcessResult<IEnumerable<Event>>> GetAllEventsAsync(int pageNumber , int pageSize);
        Task<ProcessResult<bool>> AddEventAsync(Event evt);
        Task<ProcessResult<bool>> UpdateEventAsync(Event evt);
        Task<ProcessResult<bool>> DeleteEventAsync(int id);
    }
}