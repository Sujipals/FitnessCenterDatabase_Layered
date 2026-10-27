using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class MembershipTypeRepository
    {
        private readonly DatabaseConnection _database;

        public MembershipTypeRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<MembershipType> GetAll()
        {
            List<MembershipType> types = new List<MembershipType>();

            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT MembershipTypeID, Name, Price, DurationMonths
                FROM MembershipTypes
                ORDER BY MembershipTypeID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                types.Add(new MembershipType
                {
                    MembershipTypeID = Convert.ToInt32(reader["MembershipTypeID"]),
                    Name = reader["Name"].ToString() ?? "",
                    Price = Convert.ToDecimal(reader["Price"]),
                    DurationMonths = Convert.ToInt32(reader["DurationMonths"])
                });
            }

            return types;
        }

        public bool Exists(int id)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM MembershipTypes WHERE MembershipTypeID = @ID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }
}
