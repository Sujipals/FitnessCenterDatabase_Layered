using FitnessCenterDatabase.Data;
using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Repositories
{
    public class AuthenticationRepository
    {
        private readonly DatabaseConnection _database;

        public AuthenticationRepository(DatabaseConnection database)
        {
            _database = database;
        }

        public bool TestConnection(
            string username,
            string password)
        {
            string connectionString =
                $"Server=localhost;" +
                $"Port=3306;" +
                $"Database=FitnessCenter;" +
                $"User ID={username};" +
                $"Password={password};";

            try
            {
                using MySqlConnection connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}