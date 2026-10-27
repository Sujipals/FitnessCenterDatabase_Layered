using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class BookingRepository
    {
        private readonly DatabaseConnection _database;

        public BookingRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>();

            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT BookingID, MemberID, ClassID, BookingDate
                FROM Bookings
                ORDER BY BookingID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                bookings.Add(new Booking
                {
                    BookingID = Convert.ToInt32(reader["BookingID"]),
                    MemberID = Convert.ToInt32(reader["MemberID"]),
                    ClassID = Convert.ToInt32(reader["ClassID"]),
                    BookingDate = Convert.ToDateTime(reader["BookingDate"])
                });
            }

            return bookings;
        }

        public int Insert(int memberId, int classId)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                INSERT INTO Bookings
                    (MemberID, ClassID, BookingDate)
                VALUES
                    (@MemberID, @ClassID, NOW());

                SELECT LAST_INSERT_ID();";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@MemberID", memberId);
            command.Parameters.AddWithValue("@ClassID", classId);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public bool Exists(int bookingId)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Bookings WHERE BookingID = @BookingID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@BookingID", bookingId);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public bool MemberAlreadyBooked(int memberId, int classId)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM Bookings
                WHERE MemberID = @MemberID
                  AND ClassID = @ClassID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@MemberID", memberId);
            command.Parameters.AddWithValue("@ClassID", classId);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }
}
