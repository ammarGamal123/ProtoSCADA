using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSCADA.Entities.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; }  = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ICollection<Alert> Alerts { get; set; } = new HashSet<Alert>();

    }
}
