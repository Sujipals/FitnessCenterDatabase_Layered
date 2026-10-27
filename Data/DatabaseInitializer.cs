using MySql.Data.MySqlClient;

namespace FitnessCenterDatabase.Data
{
    public class DatabaseInitializer
    {
        private readonly string _serverConnectionString;

        public DatabaseInitializer(string serverConnectionString)
        {
            _serverConnectionString = serverConnectionString;
        }

        public void Initialize()
        {
            Console.WriteLine("Connecting to MySQL...");

            using MySqlConnection connection =
                new MySqlConnection(_serverConnectionString);

            connection.Open();

            Console.WriteLine("Connected to MySQL.");
            Console.WriteLine();
            Console.WriteLine("Reading SQL file...");

            string sqlFilePath =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Database",
                    "CreateDatabase.sql");

            if (!File.Exists(sqlFilePath))
            {
                throw new FileNotFoundException(
                    "CreateDatabase.sql was not found.",
                    sqlFilePath);
            }

            string script = File.ReadAllText(sqlFilePath);

            Console.WriteLine("SQL file found.");
            Console.WriteLine("Executing SQL script...");
            Console.WriteLine();

            List<string> statements = SplitStatements(script);

            int statementNumber = 0;

            foreach (string statement in statements)
            {
                if (string.IsNullOrWhiteSpace(statement))
                {
                    continue;
                }

                statementNumber++;

                Console.WriteLine(
                    $"Executing SQL statement {statementNumber}...");

                using MySqlCommand command =
                    new MySqlCommand(statement, connection);

                command.ExecuteNonQuery();
            }

            Console.WriteLine();
            Console.WriteLine("SQL script executed successfully.");
            Console.WriteLine();
        }

        private static List<string> SplitStatements(string script)
        {
            List<string> statements = new List<string>();

            string[] lines =
                script.Split(
                    new[] { "\r\n", "\n" },
                    StringSplitOptions.None);

            string current = "";

            foreach (string line in lines)
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith("--"))
                {
                    continue;
                }

                current += line + Environment.NewLine;

                if (trimmed.EndsWith(";"))
                {
                    statements.Add(
                        current.Trim().TrimEnd(';').Trim());

                    current = "";
                }
            }

            if (!string.IsNullOrWhiteSpace(current))
            {
                statements.Add(current.Trim());
            }

            return statements;
        }
    }
}
