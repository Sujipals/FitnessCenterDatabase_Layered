using FitnessCenterDatabase.Repositories;

namespace FitnessCenterDatabase.Services
{
    public class MemberService
    {
        private readonly MemberRepository _memberRepository;

        public MemberService(MemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // ============================================================
        // CHECK IF EMAIL IS AVAILABLE
        // ============================================================
        public bool IsEmailAvailable(string email)
        {
            return !_memberRepository.EmailExists(email);
        }

        // ============================================================
        // CHECK IF MEMBER CAN BE DELETED
        // ============================================================
        public bool CanDeleteMember(int memberId)
        {
            return !_memberRepository.HasBookings(memberId);
        }

        // ============================================================
        // CREATE NEW MEMBER
        // ============================================================
        public int CreateMember(
            string firstName,
            string lastName,
            string email,
            int membershipTypeId)
        {
            // Check that first name is not empty.
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException(
                    "First name cannot be empty.");
            }

            // Check that last name is not empty.
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException(
                    "Last name cannot be empty.");
            }

            // Check that email is not empty.
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(
                    "Email cannot be empty.");
            }

            // Simple email validation.
            if (!email.Contains("@"))
            {
                throw new ArgumentException(
                    "Please enter a valid email address.");
            }

            // Check that the membership type ID is valid.
            if (membershipTypeId <= 0)
            {
                throw new ArgumentException(
                    "Membership type ID must be greater than 0.");
            }

            // Check if the email is already used.
            if (!IsEmailAvailable(email))
            {
                throw new InvalidOperationException(
                    "A member with this email already exists.");
            }

            // Send the validated data to the repository.
            return _memberRepository.Insert(
                firstName,
                lastName,
                email,
                membershipTypeId);
        }

        // ============================================================
// UPDATE MEMBER
// ============================================================
public bool UpdateMember(
    int memberId,
    string firstName,
    string lastName,
    string email,
    int membershipTypeId)
{
    if (memberId <= 0)
    {
        throw new ArgumentException(
            "Member ID must be greater than 0.");
    }

    if (string.IsNullOrWhiteSpace(firstName))
    {
        throw new ArgumentException(
            "First name cannot be empty.");
    }

    if (string.IsNullOrWhiteSpace(lastName))
    {
        throw new ArgumentException(
            "Last name cannot be empty.");
    }

    if (string.IsNullOrWhiteSpace(email))
    {
        throw new ArgumentException(
            "Email cannot be empty.");
    }

    if (!email.Contains("@"))
    {
        throw new ArgumentException(
            "Please enter a valid email address.");
    }

    if (membershipTypeId <= 0)
    {
        throw new ArgumentException(
            "Membership type ID must be greater than 0.");
    }

    return _memberRepository.Update(
        memberId,
        firstName,
        lastName,
        email,
        membershipTypeId);
}
    }
}