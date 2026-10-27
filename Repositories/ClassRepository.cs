using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class ClassRepository
    {
        private readonly DatabaseConnection _database;

        public ClassRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Class> GetAll()
        {
            List<Class> classes = new List<Class>();

            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT ClassID, Name, StartTime, DurationMinutes, Capacity, TrainerID
                FROM Classes
                ORDER BY ClassID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                classes.Add(new Class
                {
                    ClassID = Convert.ToInt32(reader["ClassID"]),
                    Name = reader["Name"].ToString() ?? "",
                    StartTime = Convert.ToDateTime(reader["StartTime"]),
                    DurationMinutes = Convert.ToInt32(reader["DurationMinutes"]),
                    Capacity = Convert.ToInt32(reader["Capacity"]),
                    TrainerID = Convert.ToInt32(reader["TrainerID"])
                });
            }

            return classes;
        }

        public bool Exists(int classId)
        {
            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql =
                "SELECT COUNT(*) FROM Classes WHERE ClassID = @ClassID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ClassID", classId);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }
}
