using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IAlertRepository : IGenericRepository<Alert>
{
    Task<IEnumerable<AlertDto>> GetAllAlertsAsync(int pageNumber, int pageSize);
    Task<AlertDto> GetAlertByIdAsync(int alertId);
}