using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IAlertService
    {
        Task<ProcessResult<Alert>> GetAlertByIdAsync(int id);
        Task<ProcessResult<IEnumerable<Alert>>> GetAllAlertsAsync(int pageNumber , int pageSize);
        Task<ProcessResult<bool>> AddAlertAsync(Alert alert);
        Task<ProcessResult<bool>> UpdateAlertAsync(Alert alert);
        Task<ProcessResult<bool>> DeleteAlertAsync(int id);
    }
}