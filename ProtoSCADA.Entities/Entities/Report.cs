using ProtoSCADA.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoSCADA.Entities.Entities
{
    public class Report : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        [ForeignKey("User")]
        public int CreatedByUserID { get; set; }
        public virtual User CreatedByUser { get; set; }

        public bool IsArchived { get; set; } = false;

        [ForeignKey("Factory")]
        public int FactoryID { get; set; }
        public virtual Factory Factory { get; set; }

        [ForeignKey("Line")]
        public int LineID { get; set; }
        public virtual Line Line { get; set; }

        public string Tags { get; set; }
        public string FilePath { get; set; }
    }
}
