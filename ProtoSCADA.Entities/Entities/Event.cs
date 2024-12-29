using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Event
    {
        [Key]
        public int ID {  get; set; }

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime TimeStamp { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Machine")]
        public int MachineID {  get; set; }
        public virtual Machine Machine { get; set; }


    }
}
