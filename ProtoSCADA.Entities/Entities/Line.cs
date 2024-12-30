using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Line : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LineNumber { get; set; }

        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public virtual Factory Factory { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public bool IsActive { get; set; } = true;


        public virtual ICollection<Machine> Machines { get; set; } = new HashSet<Machine>();
        public virtual ICollection<Alert> Alerts { get; set; } = new HashSet<Alert>();
        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();

        [ForeignKey("Supervisor")]
        public int SupervisorID { get; set; }
        public virtual User Supervisor { get; set; }
    }
}