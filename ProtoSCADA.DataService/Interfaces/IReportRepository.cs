using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IReportRepository : IGenericRepository<Report>
{
    Task<IEnumerable<ReportDto>> GetAllReportsAsync(int pageNumber, int pageSize);
    Task<ReportDto> GetReportByIdAsync(int reportId);
}