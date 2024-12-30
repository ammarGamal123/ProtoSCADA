using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Metric : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public float Value { get; set; }

        [ForeignKey("Machine")]
        public int MachineID { get; set; }
        public virtual Machine Machine { get; set; }
    }
}