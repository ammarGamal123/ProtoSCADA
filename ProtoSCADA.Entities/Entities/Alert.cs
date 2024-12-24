using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Alert
    {
        [Key]
        public int ID { get; set; }

        public float ThersholdValue {  get; set; }

        public AlertCondition Condition { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("Machine")]
        public int MachineID { get; set; }
        public Machine Machine { get; set; }

    }

    public enum AlertCondition : byte // so that we can optimize storage
    {
        GreaterThan, LessThan, Equal
    }
}
