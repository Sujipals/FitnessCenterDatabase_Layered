using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Services
{
    public class SeedService
    {
        private readonly string _connectionString;

        public SeedService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Seed()
        {
            Console.WriteLine("Seeding test data...");
            Console.WriteLine("Adding test data...");

            using MySqlConnection connection =
                new MySqlConnection(_connectionString);

            connection.Open();

            InsertMembershipTypes(connection);
            InsertTrainers(connection);
            InsertClasses(connection);
            InsertMembers(connection);
            InsertBookings(connection);

            Console.WriteLine("Test data added successfully.");
            Console.WriteLine();
        }

        private void InsertMembershipTypes(MySqlConnection connection)
        {
            string sql = @"
                INSERT IGNORE INTO MembershipTypes
                    (MembershipTypeID, Name, Price, DurationMonths)
                VALUES
                    (1, 'Basic', 149.00, 1),
                    (2, 'Premium', 299.00, 1),
                    (3, 'Annual', 2999.00, 12),
                    (10, 'Student', 199.00, 1);";

            Execute(connection, sql);
        }

        private void InsertTrainers(MySqlConnection connection)
        {
            string sql = @"
                INSERT IGNORE INTO Trainers
                    (TrainerID, FirstName, LastName, Specialization)
                VALUES
                    (1, 'Peter', 'Jensen', 'Strength Training'),
                    (2, 'Maria', 'Hansen', 'Yoga'),
                    (3, 'Thomas', 'Nielsen', 'CrossFit'),
                    (10, 'Lars', 'Andersen', 'Personal Training');";

            Execute(connection, sql);
        }

        private void InsertClasses(MySqlConnection connection)
        {
            string sql = @"
                INSERT IGNORE INTO Classes
                    (ClassID, Name, StartTime, DurationMinutes, Capacity, TrainerID)
                VALUES
                    (1, 'Strength Training', '2026-09-22 17:00:00', 60, 20, 1),
                    (2, 'Yoga', '2026-09-23 18:00:00', 60, 15, 2),
                    (3, 'CrossFit', '2026-09-24 19:00:00', 60, 15, 3),
                    (10, 'Personal Training', '2026-09-25 17:00:00', 60, 10, 10);";

            Execute(connection, sql);
        }

        private void InsertMembers(MySqlConnection connection)
        {
            string sql = @"
                INSERT IGNORE INTO Members
                    (MemberID, FirstName, LastName, Email, MembershipTypeID)
                VALUES
                    (1, 'Anna', 'Jensen', 'anna@example.com', 2),
                    (2, 'John', 'Hansen', 'john@example.com', 1),
                    (3, 'Sara', 'Nielsen', 'sara@example.com', 3),
                    (10, 'Sofie', 'Pedersen', 'sofie@example.com', 10);";

            Execute(connection, sql);
        }

        private void InsertBookings(MySqlConnection connection)
        {
            string sql = @"
                INSERT IGNORE INTO Bookings
                    (BookingID, MemberID, ClassID, BookingDate)
                VALUES
                    (1, 1, 1, '2026-09-21 10:00:00'),
                    (2, 2, 2, '2026-09-21 10:30:00'),
                    (3, 3, 3, '2026-09-21 11:00:00'),
                    (10, 10, 10, '2026-09-22 09:01:58');";

            Execute(connection, sql);
        }

        private static void Execute(
            MySqlConnection connection,
            string sql)
        {
            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.ExecuteNonQuery();
        }
    }
}
