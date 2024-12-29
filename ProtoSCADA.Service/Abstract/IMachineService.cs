using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IMachineService
    {
        Task<ProcessResult<Machine>>GetMachineByIdAsync(int id);
        Task<ProcessResult<IEnumerable<Machine>>> GetAllMachinesAsync();
        Task<ProcessResult<bool>> AddMachineAsync(Machine machine);
        Task<ProcessResult<bool>> UpdateMachineAsync(Machine machine);
        Task<ProcessResult<bool>> DeleteMachineAsync(int id);
    }
}
