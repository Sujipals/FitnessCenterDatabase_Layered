using FitnessCenterDatabase.Repositories;

namespace FitnessCenterDatabase.Services
{
    public class ValidationService
    {
        private readonly ValidationRepository _repository;

        public ValidationService(ValidationRepository repository)
        {
            _repository = repository;
        }

        public bool IsValidMemberId(int memberId)
        {
            if (memberId <= 0)
            {
                return false;
            }

            return _repository.MemberExists(memberId);
        }

        public bool IsValidMembershipTypeId(int membershipTypeId)
        {
            if (membershipTypeId <= 0)
            {
                return false;
            }

            return _repository.MembershipTypeExists(
                membershipTypeId);
        }
    }
}