using FitnessCenterDatabase.Repositories;

namespace FitnessCenterDatabase.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly MemberRepository _memberRepository;
        private readonly ClassRepository _classRepository;

        public BookingService(
            BookingRepository bookingRepository,
            MemberRepository memberRepository,
            ClassRepository classRepository)
        {
            _bookingRepository = bookingRepository;
            _memberRepository = memberRepository;
            _classRepository = classRepository;
        }

        public int CreateBooking(int memberId, int classId)
        {
            if (!_memberRepository.GetAll().Any(m => m.MemberID == memberId))
            {
                throw new InvalidOperationException("Member does not exist.");
            }

            if (!_classRepository.Exists(classId))
            {
                throw new InvalidOperationException("Class does not exist.");
            }

            if (_bookingRepository.MemberAlreadyBooked(memberId, classId))
            {
                throw new InvalidOperationException(
                    "This member is already booked for this class.");
            }

            return _bookingRepository.Insert(memberId, classId);
        }

        public void TestBookingAndLogging(LogService logService)
{
    int memberId = 10;
    int classId = 3;

    if (_bookingRepository.MemberAlreadyBooked(memberId, classId))
    {
        Console.WriteLine(
            "Test booking already exists. No new INSERT performed.");

        logService.Log(
            "INFO",
            "Bookings",
            $"Booking already exists. MemberID={memberId}, ClassID={classId}");

        return;
    }

    int bookingId = CreateBooking(memberId, classId);

    Console.WriteLine(
        $"Real INSERT performed. BookingID={bookingId}");

    logService.Log(
        "INSERT",
        "Bookings",
        $"BookingID={bookingId}, MemberID={memberId}, ClassID={classId}");

    Console.WriteLine();
}
    }
}
