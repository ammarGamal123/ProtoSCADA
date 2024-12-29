using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProtoSCADA.Service.Abstract;
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

 /*       // POST: api/machine
        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] Machine machine)
        {
            if (machine == null)
            {
                _logger.LogWarning("Received null request body for machine creation.");
                return BadRequest("Machine data cannot be null.");
            }

            try
            {
                var result = await _machineService.AddMachineAsync(machine);
                return CreatedAtAction(nameof(GetMachineById), new { id = machine.ID }, machine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a machine.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
*/
        // GET: api/machine/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineById(int id)
        {
            try
            {
                var machine = await _machineService.GetMachineByIdAsync(id);
                if (machine == null)
                {
                    _logger.LogWarning($"Machine with ID {id} not found.");
                    return NotFound($"Machine with ID {id} not found.");
                }

                return Ok(machine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the machine.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

/*        // PUT: api/machine/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, [FromBody] Machine machine)
        {
            if (machine == null)
            {
                _logger.LogWarning("Received null request body for machine update.");
                return BadRequest("Machine data cannot be null.");
            }

            try
            {
                var updatedMachine = await _machineService.UpdateMachineAsync( machine);
                if (updatedMachine == null)
                {
                    _logger.LogWarning($"Machine with ID {id} not found for update.");
                    return NotFound($"Machine with ID {id} not found.");
                }

                return Ok(updatedMachine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating machine with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
*/
        // DELETE: api/machine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            try
            {
                var deleted = await _machineService.DeleteMachineAsync(id);
                if (!deleted.IsSuccess)
                {
                    _logger.LogWarning($"Machine with ID {id} not found for deletion.");
                    return NotFound($"Machine with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting machine with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET: api/machine
        [HttpGet]
        public async Task<IActionResult> GetAllMachines()
        {
            try
            {
                var machines = await _machineService.GetAllMachinesAsync();
                return Ok(machines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving machines.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
