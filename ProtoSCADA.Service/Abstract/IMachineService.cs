using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;

public interface IMachineService
{
    Task<ProcessResult<Machine>> GetMachineByIdAsync(int id);
    Task<ProcessResult<IEnumerable<MachineDto>>> GetAllMachinesAsync(int pageNumber, int pageSize);
    Task<ProcessResult<bool>> AddMachineAsync(Machine machine);
    Task<ProcessResult<bool>> UpdateMachineAsync(Machine machine);
    Task<ProcessResult<bool>> DeleteMachineAsync(int id);
}
