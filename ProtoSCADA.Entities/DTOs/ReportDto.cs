namespace ProtoSCADA.Entities.DTOs
{
    public class ReportDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsArchived { get; set; }
        public int FactoryID { get; set; }
        public int LineID { get; set; }
        public string Tags { get; set; }
        public string FilePath { get; set; }
    }
}