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
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryService _factoryService;
        private readonly ILogger<FactoryController> _logger;

        public FactoryController(IFactoryService factoryService, ILogger<FactoryController> logger)
        {
            _factoryService = factoryService ?? throw new ArgumentNullException(nameof(factoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves a factory by its ID.
        /// </summary>
        /// <param name="id">The ID of the factory.</param>
        /// <returns>A single factory DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<FactoryDto>>> GetFactoryById(int id)
        {
            _logger.LogInformation($"Fetching factory with ID: {id}");

            try
            {
                var result = await _factoryService.GetFactoryByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Factory with ID {id} not found.");
                    return NotFound(ProcessResult<FactoryDto>.Failure("Factory not found."));
                }

                var factoryDto = MapToDto(result.Data); // Map Factory to FactoryDto
                return Ok(ProcessResult<FactoryDto>.Success(factoryDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving factory with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<FactoryDto>.Failure("An error occurred while processing your request."));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<FactoryDto>>>> GetAllFactories(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all factories.");

            try
            {
                var result = await _factoryService.GetAllFactoriesAsync(pageNumber, pageSize);

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No factories found.");
                    return NotFound(ProcessResult<IEnumerable<FactoryDto>>.Failure("No factories found."));
                }

                var factoryDtos = result.Data;
                return Ok(ProcessResult<IEnumerable<FactoryDto>>.Success(factoryDtos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving factories.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<FactoryDto>>.Failure("An error occurred while processing your request."));
            }
        }
        /// <summary>
        /// Deletes a factory by its ID.
        /// </summary>
        /// <param name="id">The ID of the factory to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteFactory(int id)
        {
            _logger.LogInformation($"Deleting factory with ID: {id}");

            try
            {
                var result = await _factoryService.DeleteFactoryAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Factory with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<bool>.Failure("Factory not found."));
                }

                _logger.LogInformation($"Factory with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting factory with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<bool>.Failure("An error occurred while processing your request."));
            }
        }
        /// <summary>
        /// Maps a Factory entity to a FactoryDto.
        /// </summary>
        private static FactoryDto MapToDto(Factory factory)
        {
            return new FactoryDto
            {
                ID = factory.ID,
                Name = factory.Name,
                Location = factory.Location
            };
        }
    }
}