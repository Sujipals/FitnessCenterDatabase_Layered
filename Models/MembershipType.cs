namespace FitnessCenterDatabase.Models
{
    public class MembershipType
    {
        public int MembershipTypeID { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
    }
}
