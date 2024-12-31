using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public class ReportRepository : GenericRepository<Report>, IReportRepository
{
    public ReportRepository(ApplicationDbContext context) : base(context) { }

    public async Task<ReportDto> GetReportByIdAsync(int reportId)
    {
        return await _dbSet
            .Include(r => r.CreatedByUser)
            .Include(r => r.Factory)
            .Include(r => r.Line)
            .Where(r => r.ID == reportId)
            .Select(r => new ReportDto
            {
                ID = r.ID,
                Name = r.Name,
                Description = r.Description,
                Type = r.Type,
                CreatedByUserID = r.CreatedByUserID,
                IsArchived = r.IsArchived,
                FactoryID = r.FactoryID,
                LineID = r.LineID,
                Tags = r.Tags,
                FilePath = r.FilePath
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReportDto>> GetAllReportsAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(r => r.CreatedByUser)
            .Include(r => r.Factory)
            .Include(r => r.Line)
            .Select(r => new ReportDto
            {
                ID = r.ID,
                Name = r.Name,
                Description = r.Description,
                Type = r.Type,
                CreatedByUserID = r.CreatedByUserID,
                IsArchived = r.IsArchived,
                FactoryID = r.FactoryID,
                LineID = r.LineID,
                Tags = r.Tags,
                FilePath = r.FilePath
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}