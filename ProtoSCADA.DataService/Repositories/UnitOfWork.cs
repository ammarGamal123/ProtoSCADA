using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<User> Users { get; private set; }

        public IGenericRepository<Machine> Machines { get; private set; }

        public IGenericRepository<Metric> Metrics { get; private set; }

        public IGenericRepository<Factory> Factorys { get; private set; }

        public IGenericRepository<Alert> Alerts { get; private set; }

        public IGenericRepository<Event> Events { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Machines = new GenericRepository<Machine>(_context);
            Metrics = new GenericRepository<Metric>(_context);
            Factorys = new GenericRepository<Factory>(_context);
            Alerts = new GenericRepository<Alert>(_context);
            Events = new GenericRepository<Event>(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error saving changes: {ex.Message}", ex);
            }
        }
    }
}