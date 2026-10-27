using FitnessCenterDatabase.Data;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class ValidationRepository
    {
        private readonly DatabaseConnection _database;

        public ValidationRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public bool MemberExists(int memberId)
        {
            using MySqlConnection connection =
                _database.CreateConnection();

            connection.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM Members
                WHERE MemberID = @MemberID;";

            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@MemberID",
                memberId);

            int count =
                Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }

        public bool MembershipTypeExists(int membershipTypeId)
        {
            using MySqlConnection connection =
                _database.CreateConnection();

            connection.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM MembershipTypes
                WHERE MembershipTypeID = @MembershipTypeID;";

            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@MembershipTypeID",
                membershipTypeId);

            int count =
                Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
    }
}