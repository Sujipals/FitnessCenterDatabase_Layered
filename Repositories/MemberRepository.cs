using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class MemberRepository
    {
        private readonly DatabaseConnection _database;

        public MemberRepository(DatabaseConnection database)
        {
            _database = database;
        }

        // ============================================================
        // GET ALL MEMBERS
        // ============================================================
        public List<Member> GetAll()
        {
            List<Member> members = new List<Member>();

            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT MemberID, FirstName, LastName, Email, MembershipTypeID
                FROM Members
                ORDER BY MemberID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                members.Add(new Member
                {
                    MemberID = Convert.ToInt32(reader["MemberID"]),
                    FirstName = reader["FirstName"].ToString() ?? "",
                    LastName = reader["LastName"].ToString() ?? "",
                    Email = reader["Email"].ToString() ?? "",
                    MembershipTypeID = Convert.ToInt32(reader["MembershipTypeID"])
                });
            }

            return members;
        }

        // ============================================================
        // CHECK IF EMAIL ALREADY EXISTS
        // ============================================================
        public bool EmailExists(string email)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Members WHERE Email = @Email;";

            using MySqlCommand command = new MySqlCommand(sql, connection);

            // Parameter protects the query from SQL Injection.
            command.Parameters.AddWithValue("@Email", email);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        // ============================================================
        // CHECK IF MEMBER HAS BOOKINGS
        // ============================================================
        public bool HasBookings(int memberId)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Bookings WHERE MemberID = @MemberID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);

            // Parameter protects the query from SQL Injection.
            command.Parameters.AddWithValue("@MemberID", memberId);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        // ============================================================
        // INSERT NEW MEMBER
        // ============================================================
        public int Insert(
            string firstName,
            string lastName,
            string email,
            int membershipTypeId)
        {
            // SQL command with parameters.
            string sql = @"
                INSERT INTO Members
                (FirstName, LastName, Email, MembershipTypeID)
                VALUES
                (@FirstName, @LastName, @Email, @MembershipTypeID);

                SELECT LAST_INSERT_ID();";

            // Create a connection to MySQL.
            using MySqlConnection connection = _database.CreateConnection();

            // Open the database connection.
            connection.Open();

            // Create the SQL command.
            using MySqlCommand command =
                new MySqlCommand(sql, connection);

            // Add user input as parameters.
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue(
                "@MembershipTypeID",
                membershipTypeId);

            // ExecuteScalar returns the newly created MemberID.
            return Convert.ToInt32(command.ExecuteScalar());
        }

            // ============================================================
            // UPDATE MEMBER
            // ============================================================
            public bool Update(
            int memberId,
            string firstName,
            string lastName,
            string email,
            int membershipTypeId)
            {
                string sql = @"
                    UPDATE Members
                    SET
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    MembershipTypeID = @MembershipTypeID
                    WHERE MemberID = @MemberID;";

                using MySqlConnection connection = _database.CreateConnection();

                connection.Open();

                using MySqlCommand command =
                    new MySqlCommand(sql, connection);

                // Add values as parameters.
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue(
                    "@MembershipTypeID",
                 membershipTypeId);
                command.Parameters.AddWithValue("@MemberID", memberId);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
    }
}