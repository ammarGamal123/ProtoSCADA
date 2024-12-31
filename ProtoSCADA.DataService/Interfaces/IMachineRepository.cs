using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IMachineRepository : IGenericRepository<Machine>
{
    Task<IEnumerable<MachineDto>> GetAllMachinesAsync(int pageNumber, int pageSize);
    Task<Machine> GetMachineByIdAsync(int machineId);
}