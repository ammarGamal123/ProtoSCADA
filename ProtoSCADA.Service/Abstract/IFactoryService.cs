using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IFactoryService
    {
        Task<ProcessResult<Factory>> GetFactorByIdAsync(int id);
        Task<ProcessResult<IEnumerable<Factory>>> GetAllFactorysAsync();
        Task<ProcessResult<bool>> AddFactoryAsync(Factory factory);
        Task<ProcessResult<bool>> UpdateFactoryAsync(Factory factory);
        Task<ProcessResult<bool>> DeleteFactoryAsync(int id);
    }
}
