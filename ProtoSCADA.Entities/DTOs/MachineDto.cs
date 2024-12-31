using ProtoSCADA.Entities.Entities;

namespace ProtoSCADA.Entities.DTOs
{
    public class MachineDto
    {
        public int MachineID { get; set; }
        public string MachineType { get; set; } = string.Empty;
        public MachineStatus Status { get; set; }
        public DateTime LastMaintance { get; set;}
        public string FactorName { get; set; } = string.Empty;
        public string LineName { get; set; } = string.Empty; // Name of the associated line

    }
}
