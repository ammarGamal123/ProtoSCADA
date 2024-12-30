using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Machine : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public MachineStatus Status { get; set; }
        public DateTime LastMaintenance { get; set; }

        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public virtual Factory Factory { get; set; }

        [ForeignKey("Line")]
        public int LineID { get; set; }
        public virtual Line Line { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; } = new HashSet<Alert>();
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public virtual ICollection<Metric> Metrics { get; set; } = new HashSet<Metric>();
    }

    public enum MachineStatus : byte
    {
        Running, Idle, Stopped
    }
}