using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Entities.Entities;

public interface IFactoryRepository : IGenericRepository<Factory>
{
    Task<IEnumerable<FactoryDto>> GetAllFactoriesAsync(int pageNumber, int pageSize);
    Task<FactoryDto> GetFactoryByIdAsync(int factoryId);
}