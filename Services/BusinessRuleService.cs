using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Services
{
    public class BusinessRuleService
    {
        private readonly string _connectionString;

        public BusinessRuleService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsEmailAvailable(string email)
        {
            using MySqlConnection connection =
                new MySqlConnection(_connectionString);

            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Members WHERE Email = @Email;";

            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Email", email);

            return Convert.ToInt32(command.ExecuteScalar()) == 0;
        }

        public bool MembershipTypeExists(int id)
        {
            using MySqlConnection connection =
                new MySqlConnection(_connectionString);

            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM MembershipTypes WHERE MembershipTypeID = @ID;";

            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ID", id);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public bool IsValidCapacity(int capacity)
        {
            return capacity > 0;
        }

        public bool CanDeleteMember(int memberId)
        {
            using MySqlConnection connection =
                new MySqlConnection(_connectionString);

            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Bookings WHERE MemberID = @MemberID;";

            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@MemberID", memberId);

            return Convert.ToInt32(command.ExecuteScalar()) == 0;
        }

        public void TestRules()
        {
            Console.WriteLine("Testing business rules...");

            Console.WriteLine(
                $"Email 'newmember@example.com' available: " +
                $"{IsEmailAvailable("newmember@example.com")}");

            Console.WriteLine(
                $"Membership type 1 exists: " +
                $"{MembershipTypeExists(1)}");

            Console.WriteLine(
                $"Capacity 20 is valid: " +
                $"{IsValidCapacity(20)}");

            Console.WriteLine(
                $"Member 1 can be deleted: " +
                $"{CanDeleteMember(1)}");

            Console.WriteLine();
        }
    }
}
