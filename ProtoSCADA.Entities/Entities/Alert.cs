using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Alert : BaseEntity
    {
        public float ThresholdValue { get; set; }
        public AlertCondition Condition { get; set; }

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

    public enum AlertCondition : byte
    {
        GreaterThan, LessThan, Equal
    }
}
