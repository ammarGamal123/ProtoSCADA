using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;

        public EventService(IUnitOfWork unitOfWork, IEventRepository eventRepository = null)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
        }

        public async Task<ProcessResult<bool>> AddEventAsync(Event evnt)
        {
            try
            {
                if (evnt == null)
                    return ProcessResult<bool>.Failure("Event cannot be null.");

                await _unitOfWork.Events.AddAsync(evnt);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error adding event: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> DeleteEventAsync(int id)
        {
            try
            {
                var evnt = await _unitOfWork.Events.GetByIdAsync(id);
                if (evnt == null)
                    return ProcessResult<bool>.Failure($"Event with ID {id} not found.");

                await _unitOfWork.Events.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error deleting event: {ex.Message}");
            }
        }

        public async Task<ProcessResult<IEnumerable<Event>>> GetAllEventsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var validationResult = ValidatePagination.Validate(pageNumber, pageSize);
                if (!validationResult.IsSuccess)
                {
                    return ProcessResult<IEnumerable<Event>>.Failure(validationResult.Message);
                }
                var events = await _eventRepository.GetAllAsync();
                var paginatedEvents = events
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return ProcessResult<IEnumerable<Event>>.Success(paginatedEvents);
            }
            catch (Exception ex)
            {
                return ProcessResult<IEnumerable<Event>>.Failure($"Error retrieving events: {ex.Message}");
            }
        }

        public async Task<ProcessResult<Event>> GetEventByIdAsync(int id)
        {
            try
            {
                var evnt = await _eventRepository.GetByIdAsync(id);
                if (evnt == null)
                    return ProcessResult<Event>.Failure($"Event with ID {id} not found.");

                return ProcessResult<Event>.Success(evnt);
            }
            catch (Exception ex)
            {
                return ProcessResult<Event>.Failure($"Error retrieving event: {ex.Message}");
            }
        }

        public async Task<ProcessResult<bool>> UpdateEventAsync(Event evnt)
        {
            try
            {
                if (evnt == null)
                    return ProcessResult<bool>.Failure("Event cannot be null.");

                _unitOfWork.Events.Update(evnt);
                await _unitOfWork.SaveAsync();
                return ProcessResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ProcessResult<bool>.Failure($"Error updating event: {ex.Message}");
            }
        }
    }
}