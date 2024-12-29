using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Factory
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet <User>();    

        public virtual ICollection<Machine> Machines { get; set; } = new HashSet<Machine>();

    }
}
