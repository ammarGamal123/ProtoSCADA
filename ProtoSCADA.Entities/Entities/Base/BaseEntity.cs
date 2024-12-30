using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
