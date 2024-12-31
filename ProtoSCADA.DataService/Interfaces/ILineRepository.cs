using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface ILineRepository : IGenericRepository<Line>
{
    Task<IEnumerable<LineDto>> GetAllLinesAsync(int pageNumber, int pageSize);
    Task<LineDto> GetLineByIdAsync(int lineId);
}