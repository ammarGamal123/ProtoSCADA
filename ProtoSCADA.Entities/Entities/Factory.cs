using ProtoSCADA.Entities.Entities.Base;

namespace ProtoSCADA.Entities.Entities
{
    public class Factory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<Machine> Machines { get; set; } = new HashSet<Machine>();
        public virtual ICollection<Line> Lines { get; set; } = new HashSet<Line>();
        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();
        public virtual ICollection<Alert> Alerts { get; set; } = new HashSet<Alert>();
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}