using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Models;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class TrainerRepository
    {
        private readonly DatabaseConnection _database;

        public TrainerRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public List<Trainer> GetAll()
        {
            List<Trainer> trainers = new List<Trainer>();

            using MySqlConnection connection = _database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT TrainerID, FirstName, LastName, Specialization
                FROM Trainers
                ORDER BY TrainerID;";

            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                trainers.Add(new Trainer
                {
                    TrainerID = Convert.ToInt32(reader["TrainerID"]),
                    FirstName = reader["FirstName"].ToString() ?? "",
                    LastName = reader["LastName"].ToString() ?? "",
                    Specialization = reader["Specialization"].ToString() ?? ""
                });
            }

            return trainers;
        }
    }
}
