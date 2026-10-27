using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace FitnessCenterDatabase.Repositories
{
    public class StoredProcedureRepository
    {
        private readonly DatabaseConnection _database;

        public StoredProcedureRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public Member? GetMemberById(int memberId)
        {
            using MySqlConnection connection = _database.CreateConnection();

            connection.Open();

            using MySqlCommand command =new MySqlCommand("GetMemberById", connection);

            // Tell MySQL that we are calling a stored procedure.
            command.CommandType = CommandType.StoredProcedure;

            // Send the member ID to the stored procedure.
            command.Parameters.AddWithValue("@p_MemberID",memberId);

            using MySqlDataReader reader =command.ExecuteReader();

            // If no member was found, return null.
            if (!reader.Read())
            {
                return null;
            }

            // Convert the database row into a Member object.
            Member member = new Member
            {
                MemberID = Convert.ToInt32(
                    reader["MemberID"]),

                FirstName = reader["FirstName"]
                    .ToString() ?? "",

                LastName = reader["LastName"]
                    .ToString() ?? "",

                Email = reader["Email"]
                    .ToString() ?? "",

                MembershipTypeID = Convert.ToInt32(reader["MembershipTypeID"])
            };

            return member;
        }
    }
}