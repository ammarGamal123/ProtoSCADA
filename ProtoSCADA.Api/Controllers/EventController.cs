using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Entities.Entities;
using Microsoft.Extensions.Logging;

namespace ProtoSCADA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>A single event DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<EventDto>>> GetEventById(int id)
        {
            _logger.LogInformation($"Fetching event with ID: {id}");

            try
            {
                var result = await _eventService.GetEventByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Event with ID {id} not found.");
                    return NotFound(ProcessResult<EventDto>.Failure("Event not found."));
                }

                var eventDto = MapToDto(result.Data); // Map Event to EventDto
                return Ok(ProcessResult<EventDto>.Success(eventDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving event with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<EventDto>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Retrieves all events.
        /// </summary>
        /// <returns>A list of event DTOs wrapped in a ProcessResult.</returns>

        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<EventDto>>>> GetAllEvents(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all events.");

            try
            {
                var result = await _eventService.GetAllEventsAsync(pageNumber, pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No events found.");
                    return NotFound(ProcessResult<IEnumerable<EventDto>>.Failure("No events found."));
                }

                var eventDtos = result.Data.Select(MapToDto).ToList(); // Map IEnumerable<Event> to IEnumerable<EventDto>
                return Ok(ProcessResult<IEnumerable<EventDto>>.Success(eventDtos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving events.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<EventDto>>.Failure("An error occurred while processing your request."));
            }
        }


        /// <summary>
        /// Deletes an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteEvent(int id)
        {
            _logger.LogInformation($"Deleting event with ID: {id}");

            try
            {
                var result = await _eventService.DeleteEventAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Event with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("Event not found."));
                }

                _logger.LogInformation($"Event with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting event with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }

        private static EventDto MapToDto(Event eventEntity)
        {
            return new EventDto
            {
                ID = eventEntity.ID,
                Type = eventEntity.Type,
                Description = eventEntity.Description,
                UserID = eventEntity.UserID,
                MachineID = eventEntity.MachineID,
                LineID = eventEntity.LineID,
                FactoryID = eventEntity.FactoryID
            };
        }
    }
}