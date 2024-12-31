using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Service.Abstract
{
    public interface IReportService
    {
        Task<ProcessResult<Report>> GetReportByIdAsync(int id);
        Task<ProcessResult<IEnumerable<ReportDto>>> GetAllReportsAsync(int pageNumber , int pageSize);
        Task<ProcessResult<bool>> AddReportAsync(Report report);
        Task<ProcessResult<bool>> UpdateReportAsync(Report report);
        Task<ProcessResult<bool>> DeleteReportAsync(int id);
    }
}