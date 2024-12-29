using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Repositories
{
    public class MachineRepository : GenericRepository<Machine> , IMachineRepository
    {
        public MachineRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
        {
            try
            {
                return await _dbSet.Include(m => m.Factory)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Machine> GetMachineByIdAsync(int machineId)
        {
            try
            {
                return await _dbSet.Include(m => m.Factory).FirstOrDefaultAsync(m => m.ID == machineId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
