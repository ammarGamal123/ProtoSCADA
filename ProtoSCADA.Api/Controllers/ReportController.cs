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
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves a report by its ID.
        /// </summary>
        /// <param name="id">The ID of the report.</param>
        /// <returns>A single report DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<ReportDto>>> GetReportById(int id)
        {
            _logger.LogInformation($"Fetching report with ID: {id}");

            try
            {
                var result = await _reportService.GetReportByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Report with ID {id} not found.");
                    return NotFound(ProcessResult<ReportDto>.Failure("Report not found."));
                }

                var reportDto = MapToDto(result.Data); // Map Report to ReportDto
                return Ok(ProcessResult<ReportDto>.Success(reportDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving report with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<ReportDto>.Failure("An error occurred while processing your request."));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<ReportDto>>>> GetAllReports(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all reports.");

            try
            {
                var result = await _reportService.GetAllReportsAsync(pageNumber, pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No reports found.");
                    return NotFound(ProcessResult<IEnumerable<ReportDto>>.Failure("No reports found."));
                }

                var reportDtos = result.Data; 
                return Ok(ProcessResult<IEnumerable<ReportDto>>.Success(reportDtos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving reports.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<ReportDto>>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Maps a Report entity to a ReportDto.
        /// </summary>
        private static ReportDto MapToDto(Report report)
        {
            return new ReportDto
            {
                ID = report.ID,
                Name = report.Name,
                Description = report.Description,
                Type = report.Type,
                CreatedByUserID = report.CreatedByUserID,
                IsArchived = report.IsArchived,
                FactoryID = report.FactoryID,
                LineID = report.LineID,
                Tags = report.Tags,
                FilePath = report.FilePath
            };
        }

        /// <summary>
        /// Deletes a report by its ID.
        /// </summary>
        /// <param name="id">The ID of the report to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteReport(int id)
        {
            _logger.LogInformation($"Deleting report with ID: {id}");

            try
            {
                var result = await _reportService.DeleteReportAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Report with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("Report not found."));
                }

                _logger.LogInformation($"Report with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting report with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }
    }
}