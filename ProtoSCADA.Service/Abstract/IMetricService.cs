using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IMetricService
    {
        Task<ProcessResult<Metric>> GetMetricByIdAsync(int id);
        Task<ProcessResult<IEnumerable<Metric>>> GetAllMetricsAsync();
        Task<ProcessResult<bool>> AddMetricAsync(Metric metric);
        Task<ProcessResult<bool>> UpdateMetricAsync(Metric metric);
        Task<ProcessResult<bool>> DeleteMetricAsync(int id);
    }
}
