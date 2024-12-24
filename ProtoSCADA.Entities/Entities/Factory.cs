using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class Factory
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet <User>();    

        public ICollection<Machine> Machines { get; set; } = new HashSet<Machine>();

    }
}
