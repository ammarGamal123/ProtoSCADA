using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Interfaces
{
    public interface IMachineRepository : IGenericRepository<Machine>
    {
        Task<IEnumerable<Machine>> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(int machineId);
    }
}
