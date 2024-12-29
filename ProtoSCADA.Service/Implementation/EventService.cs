using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessResult<bool>> AddEventAsync(Event eventEntity)
        {
            if (eventEntity == null)
                return ProcessResult<bool>.Failure("Event cannot be null.", false);

            try
            {
                await _unitOfWork.Events.AddAsync(eventEntity);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Event added successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding event: {ex.Message}", false);
            }
        }

        public async Task<ProcessResult<bool>> DeleteEventAsync(int id)
        {
            try
            {
                var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
                if (eventEntity == null)
                    return ProcessResult<bool>.Failure($"No event found with ID {id}.", false);

                await _unitOfWork.Events.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Event deleted successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting event: {ex.Message}", false);
            }
        }

        public async Task<ProcessResult<IEnumerable<Event>>> GetAllEventsAsync()
        {
            try
            {
                var events = await _unitOfWork.Events.GetAllAsync();
                return ProcessResult<IEnumerable<Event>>.Success("Events retrieved successfully.", events);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Event>>.Failure($"Error retrieving events: {ex.Message}", null);
            }
        }

        public async Task<ProcessResult<Event>> GetEventByIdAsync(int id)
        {
            try
            {
                var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
                if (eventEntity == null)
                    return ProcessResult<Event>.Failure($"Event with ID {id} not found.", null);

                return ProcessResult<Event>.Success("Event retrieved successfully.", eventEntity);
            }
            catch (Exception ex)
            {
                return ProcessResult<Event>.Failure($"Error retrieving event: {ex.Message}", null);
            }
        }

        public async Task<ProcessResult<bool>> UpdateEventAsync(Event eventEntity)
        {
            if (eventEntity == null)
                return ProcessResult<bool>.Failure("Event cannot be null.", false);

            try
            {
                _unitOfWork.Events.Update(eventEntity);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success("Event updated successfully.", true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating event: {ex.Message}", false);
            }
        }
    }
}
