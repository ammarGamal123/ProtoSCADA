using Microsoft.EntityFrameworkCore;
using ProtoSCADA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.DataService.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        DbSet<Alert> Alerts { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Factory> Factorys { get; set; }   
        DbSet<Machine> Machines { get; set; }
        DbSet<Metric> Metrics { get; set; }
        DbSet<User> Users { get; set; } 



    }
}
