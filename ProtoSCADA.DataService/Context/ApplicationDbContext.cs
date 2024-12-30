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
        public DbSet<Factory> Factories { get; set; }   
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            // User and Factory
            modelBuilder.Entity<User>()
                .HasOne(u => u.Factory)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // User and Line
            modelBuilder.Entity<User>()
                .HasOne(u => u.Line)
                .WithMany()
                .HasForeignKey(u => u.LineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Factory and Machines
            modelBuilder.Entity<Machine>()
                .HasOne(m => m.Factory)
                .WithMany(f => f.Machines)
                .HasForeignKey(m => m.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Factory and Lines
            modelBuilder.Entity<Line>()
                .HasOne(l => l.Factory)
                .WithMany(f => f.Lines)
                .HasForeignKey(l => l.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Factory and Reports
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Factory)
                .WithMany(f => f.Reports)
                .HasForeignKey(r => r.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Factory and Alerts
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Factory)
                .WithMany(f => f.Alerts)
                .HasForeignKey(a => a.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Factory and Events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Factory)
                .WithMany(f => f.Events)
                .HasForeignKey(e => e.FactoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Line and Machines
            modelBuilder.Entity<Machine>()
                .HasOne(m => m.Line)
                .WithMany(l => l.Machines)
                .HasForeignKey(m => m.LineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Line and Supervisor (User)
            modelBuilder.Entity<Line>()
                .HasOne(l => l.Supervisor)
                .WithMany()
                .HasForeignKey(l => l.SupervisorID)
                .OnDelete(DeleteBehavior.Restrict);

            // Line and Alerts
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Line)
                .WithMany(l => l.Alerts)
                .HasForeignKey(a => a.LineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Line and Events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Line)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.LineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Line and Reports
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Line)
                .WithMany(l => l.Reports)
                .HasForeignKey(r => r.LineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Machine and Metrics
            modelBuilder.Entity<Metric>()
                .HasOne(m => m.Machine)
                .WithMany(ma => ma.Metrics)
                .HasForeignKey(m => m.MachineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Machine and Alerts
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Machine)
                .WithMany(m => m.Alerts)
                .HasForeignKey(a => a.MachineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Machine and Events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Machine)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.MachineID)
                .OnDelete(DeleteBehavior.Restrict);

            // Report and User (CreatedByUser)
            modelBuilder.Entity<Report>()
                .HasOne(r => r.CreatedByUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Event and User
            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum Conversions
            modelBuilder.Entity<Machine>()
                .Property(m => m.Status)
                .HasConversion<byte>();

            modelBuilder.Entity<Alert>()
                .Property(a => a.Condition)
                .HasConversion<byte>();

            // Default Values for CreatedAt
            modelBuilder.Entity<Alert>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Event>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            
            // Indexes
            modelBuilder.Entity<Machine>()
                .HasIndex(m => m.FactoryID)
                .HasDatabaseName("IX_Machine_FactoryID");

            modelBuilder.Entity<Line>()
                .HasIndex(l => l.FactoryID)
                .HasDatabaseName("IX_Line_FactoryID");

            modelBuilder.Entity<Report>()
                .HasIndex(r => r.FactoryID)
                .HasDatabaseName("IX_Report_FactoryID");

            modelBuilder.Entity<Alert>()
                .HasIndex(a => a.FactoryID)
                .HasDatabaseName("IX_Alert_FactoryID");

            modelBuilder.Entity<Event>()
                .HasIndex(e => e.FactoryID)
                .HasDatabaseName("IX_Event_FactoryID");
        }

    }
}
