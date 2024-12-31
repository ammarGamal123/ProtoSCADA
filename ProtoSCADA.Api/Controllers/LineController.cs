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
    public class LineController : ControllerBase
    {
        private readonly ILineService _lineService;
        private readonly ILogger<LineController> _logger;

        public LineController(ILineService lineService, ILogger<LineController> logger)
        {
            _lineService = lineService ?? throw new ArgumentNullException(nameof(lineService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves a line by its ID.
        /// </summary>
        /// <param name="id">The ID of the line.</param>
        /// <returns>A single line DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<LineDto>>> GetLineById(int id)
        {
            _logger.LogInformation($"Fetching line with ID: {id}");

            try
            {
                var result = await _lineService.GetLineByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Line with ID {id} not found.");
                    return NotFound(ProcessResult<LineDto>.Failure("Line not found."));
                }

                var lineDto = MapToDto(result.Data); // Map Line to LineDto
                return Ok(ProcessResult<LineDto>.Success(lineDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving line with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<LineDto>.Failure("An error occurred while processing your request."));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<LineDto>>>> GetAllLines(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all lines.");

            try
            {
                var result = await _lineService.GetAllLinesAsync(pageNumber, pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No lines found.");
                    return NotFound(ProcessResult<IEnumerable<LineDto>>.Failure("No lines found."));
                }

                var lineDtos = result.Data;
                return Ok(ProcessResult<IEnumerable<LineDto>>.Success(lineDtos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving lines.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<LineDto>>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Maps a Line entity to a LineDto.
        /// </summary>
        private static LineDto MapToDto(Line line)
        {
            return new LineDto
            {
                ID = line.ID,
                Name = line.Name,
                Description = line.Description,
                LineNumber = line.LineNumber,
                FactoryName = line.Factory.Name,
                LastMaintenanceDate = line.LastMaintenanceDate,
                IsActive = line.IsActive,
                SupervisorName = line.Supervisor.Name
            };
        }

        /// <summary>
        /// Deletes a line by its ID.
        /// </summary>
        /// <param name="id">The ID of the line to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteLine(int id)
        {
            _logger.LogInformation($"Deleting line with ID: {id}");

            try
            {
                var result = await _lineService.DeleteLineAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Line with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("Line not found."));
                }

                _logger.LogInformation($"Line with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting line with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }
    }
}
