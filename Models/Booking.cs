namespace FitnessCenterDatabase.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int MemberID { get; set; }
        public int ClassID { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
