namespace FitnessCenterDatabase.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string Name { get; set; } = "";
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public int Capacity { get; set; }
        public int TrainerID { get; set; }
    }
}
