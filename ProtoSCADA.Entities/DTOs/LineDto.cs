namespace ProtoSCADA.Entities.DTOs
{
    public class LineDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LineNumber { get; set; }
        public string FactoryName { get; set; } = string.Empty;
        public DateTime? LastMaintenanceDate { get; set; }
        public bool IsActive { get; set; }
        public string SupervisorName { get; set; } = string.Empty;
    }
}