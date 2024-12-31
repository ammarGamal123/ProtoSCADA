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
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        private readonly ILogger<AlertController> _logger;

        public AlertController(IAlertService alertService, ILogger<AlertController> logger)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves an alert by its ID.
        /// </summary>
        /// <param name="id">The ID of the alert.</param>
        /// <returns>A single alert DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<AlertDto>>> GetAlertById(int id)
        {
            _logger.LogInformation($"Fetching alert with ID: {id}");

            try
            {
                var result = await _alertService.GetAlertByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Alert with ID {id} not found.");
                    return NotFound(ProcessResult<AlertDto>.Failure("Alert not found."));
                }

                var alertDto = MapToDto(result.Data); // Ensure this mapping works correctly
                return Ok(ProcessResult<AlertDto>.Success(alertDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving alert with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<AlertDto>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Retrieves all alerts.
        /// </summary>
        /// <returns>A list of alert DTOs wrapped in a ProcessResult.</returns>
        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<AlertDto>>>> GetAllAlerts(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all alerts.");

            try
            {
                var result = await _alertService.GetAllAlertsAsync(pageNumber, pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No alerts found.");
                    return NotFound(ProcessResult<IEnumerable<AlertDto>>.Failure("No alerts found."));
                }
                var alertDtos = result.Data.Select(MapToDto).ToList(); // Map IEnumerable<Alert> to IEnumerable<AlertDto>
                return Ok(ProcessResult<IEnumerable<AlertDto>>.Success(alertDtos));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving alerts.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<AlertDto>>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Deletes an alert by its ID.
        /// </summary>
        /// <param name="id">The ID of the alert to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteAlert(int id)
        {
            _logger.LogInformation($"Deleting alert with ID: {id}");

            try
            {
                var result = await _alertService.DeleteAlertAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Alert with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("Alert not found."));
                }

                _logger.LogInformation($"Alert with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting alert with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }

        private static AlertDto MapToDto(Alert alert)
        {
            return new AlertDto
            {
                ID = alert.ID,
                ThresholdValue = alert.ThresholdValue,
                Condition = alert.Condition.ToString(),
                MachineID = alert.MachineID,
                LineID = alert.LineID,
                FactoryID = alert.FactoryID
            };
        }
    }
}