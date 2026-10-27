using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Services
{
    public class TriggerService
    {
        private readonly string _connectionString;

        public TriggerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateBookingInsertTrigger()
        {
            using MySqlConnection connection =
                new MySqlConnection(_connectionString);

            connection.Open();

            string dropSql =
                "DROP TRIGGER IF EXISTS trg_Booking_Insert;";

            using (MySqlCommand dropCommand =
                new MySqlCommand(dropSql, connection))
            {
                dropCommand.ExecuteNonQuery();
            }

            string createSql = @"
                CREATE TRIGGER trg_Booking_Insert
                AFTER INSERT ON Bookings
                FOR EACH ROW
                INSERT INTO BookingHistory
                    (BookingID, MemberID, ClassID, Action, ActionDate)
                VALUES
                    (NEW.BookingID, NEW.MemberID, NEW.ClassID, 'INSERT', NOW());";

            using MySqlCommand createCommand =
                new MySqlCommand(createSql, connection);

            createCommand.ExecuteNonQuery();

            Console.WriteLine(
                "Booking INSERT trigger created successfully.");
            Console.WriteLine();
        }
    }
}
