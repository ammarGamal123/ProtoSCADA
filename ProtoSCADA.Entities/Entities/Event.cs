using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Event : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Machine")]
        public int MachineID { get; set; }
        public virtual Machine Machine { get; set; }

        [ForeignKey("Line")]
        public int LineID { get; set; }
        public virtual Line Line { get; set; }

        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public virtual Factory Factory { get; set; }
    }
}
