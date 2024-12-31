namespace ProtoSCADA.Entities.DTOs
{
    public class AlertDto
    {
        public int ID { get; set; }
        public float ThresholdValue { get; set; }
        public string Condition { get; set; } // Enum as string
        public int MachineID { get; set; }
        public int LineID { get; set; }
        public int FactoryID { get; set; }
    }
}