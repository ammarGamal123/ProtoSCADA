using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Metric
    {
        [Key]
        public int ID { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Name { get; set; } = string.Empty;

        public float Value {  get; set; }


        [ForeignKey("Machine")]
        public int MachineID {  get; set; }
        public Machine Machine { get; set; }
    }
}
