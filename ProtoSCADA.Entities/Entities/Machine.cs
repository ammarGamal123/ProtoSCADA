using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Machine
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public MachineStatus Status {  get; set; }
        
        public DateTime LastMaintance { get; set; }

        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public virtual Factory Factory { get; set; }


        public virtual ICollection<Alert> Alerts { get; set; } = new HashSet<Alert>();

        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();

        public virtual ICollection<Metric> Metrics { get; set; } = new HashSet<Metric>();

    }
    public enum MachineStatus : byte // so that we can optimize storage
    {
        Running, Idle, Stopped
    }
}
