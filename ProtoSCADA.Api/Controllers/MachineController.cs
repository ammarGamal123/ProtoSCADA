using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Entities.Entities;

namespace ProtoSCADA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        private readonly ILogger<MachineController> _logger;

        public MachineController(IMachineService machineService, ILogger<MachineController> logger)
        {
            _machineService = machineService ?? throw new ArgumentNullException(nameof(machineService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves a machine by its ID.
        /// </summary>
        /// <param name="id">The ID of the machine.</param>
        /// <returns>A single machine DTO wrapped in a ProcessResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<MachineDto>>> GetMachineById(int id)
        {
            _logger.LogInformation($"Fetching machine with ID: {id}");

            try
            {
                var result = await _machineService.GetMachineByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    _logger.LogWarning($"Machine with ID {id} not found.");
                    return NotFound(ProcessResult<MachineDto>.Failure("Machine not found."));
                }

                var machineDto = MapToDto(result.Data);
                return Ok(ProcessResult<MachineDto>.Success(machineDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving machine with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<MachineDto>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Retrieves all machines.
        /// </summary>
        /// <returns>A list of machine DTOs wrapped in a ProcessResult.</returns>
        [HttpGet]
        public async Task<ActionResult<ProcessResult<IEnumerable<MachineDto>>>> GetAllMachines()
        {
            _logger.LogInformation("Fetching all machines.");

            try
            {
                var result = await _machineService.GetAllMachinesAsync();

                if (!result.IsSuccess || result.Data == null || !result.Data.Any())
                {
                    _logger.LogWarning("No machines found.");
                    return NotFound(ProcessResult<IEnumerable<MachineDto>>.Failure("No machines found."));
                }

                var machinesDto = result.Data.Select(MapToDto).ToList();
                return Ok(ProcessResult<IEnumerable<MachineDto>>.Success(machinesDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving machines.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ProcessResult<IEnumerable<MachineDto>>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Deletes a machine by its ID.
        /// </summary>
        /// <param name="id">The ID of the machine to delete.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<bool>>> DeleteMachine(int id)
        {
            _logger.LogInformation($"Deleting machine with ID: {id}");

            try
            {
                var result = await _machineService.DeleteMachineAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"Machine with ID {id} not found for deletion.");
                    return NotFound(ProcessResult<MachineDto>.Failure("Machine not found."));
                }

                _logger.LogInformation($"Machine with ID {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting machine with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Service.Utilities.ProcessResult<MachineDto>.Failure("An error occurred while processing your request."));
            }
        }

        /// <summary>
        /// Maps a machine entity to its DTO representation.
        /// </summary>
        /// <param name="machine">The machine entity.</param>
        /// <returns>The mapped MachineDto.</returns>
        private static MachineDto MapToDto(Machine machine)
        {
            return new MachineDto
            {
                MachineID = machine.ID,
                MachineType = machine.Type,
                Status = machine.Status,
                LastMaintance = machine.LastMaintance,
                FactorName = machine.Factory?.Name // Safely access Factory properties
            };
        }
    }
}
