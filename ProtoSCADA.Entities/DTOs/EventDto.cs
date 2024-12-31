namespace ProtoSCADA.Entities.DTOs
{
    public class EventDto
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int MachineID { get; set; }
        public int LineID { get; set; }
        public int FactoryID { get; set; }
    }
}