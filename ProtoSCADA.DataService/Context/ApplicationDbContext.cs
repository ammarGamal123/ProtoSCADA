using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Factory> Factorys { get; set; }   
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<User> Users { get; set; } 



    }
}
