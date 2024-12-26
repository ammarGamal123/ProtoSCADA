using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Machine> Machines { get; }
        IGenericRepository<Metric> Metrics { get; }
        IGenericRepository<Factory> Factorys { get; }
        IGenericRepository<Alert> Alerts { get; }
        IGenericRepository<Event> Events { get; }


        Task<int> SaveAsync();
        

    }
}
