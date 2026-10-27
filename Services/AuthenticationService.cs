using FitnessCenterDatabase.Repositories;

namespace FitnessCenterDatabase.Services
{
    public class AuthenticationService
    {
        private readonly AuthenticationRepository _repository;

        public AuthenticationService(
            AuthenticationRepository repository)
        {
            _repository = repository;
        }

        public string? Login(
            string username,
            string password)
        {
            bool valid =
                _repository.TestConnection(
                    username,
                    password);

            if (!valid)
            {
                return null;
            }

            if (username == "admin")
            {
                return "admin";
            }

            if (username == "fitness_user")
            {
                return "user";
            }

            return null;
        }
    }
}