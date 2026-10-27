using FitnessCenterDatabase.Models;
using FitnessCenterDatabase.Repositories;

namespace FitnessCenterDatabase.Services
{
    public class StoredProcedureService
    {
        private readonly StoredProcedureRepository _repository;

        public StoredProcedureService(
            StoredProcedureRepository repository)
        {
            _repository = repository;
        }

        public Member? GetMemberById(int memberId)
        {
            // Validate the member ID before accessing the database.
            if (memberId <= 0)
            {
                throw new ArgumentException(
                    "Member ID must be greater than 0.");
            }

            // Call the repository.
            return _repository.GetMemberById(memberId);
        }
    }
}