using FitnessCenterDatabase.Repositories;
using FitnessCenterDatabase.Services;

namespace FitnessCenterDatabase.Presentation
{
    public class ConsoleUI
    {
        private readonly MemberRepository _members;
        private readonly TrainerRepository _trainers;
        private readonly ClassRepository _classes;
        private readonly BookingRepository _bookings;
        private readonly MembershipTypeRepository _membershipTypes;
        private readonly MemberService _memberService;
        private readonly BookingService _bookingService;
        private readonly AuthenticationService _authenticationService;

        public ConsoleUI(
            MemberRepository members,
            TrainerRepository trainers,
            ClassRepository classes,
            BookingRepository bookings,
            MembershipTypeRepository membershipTypes,
            MemberService memberService,
            BookingService bookingService,
            AuthenticationService authenticationService)
        {
            _members = members;
            _trainers = trainers;
            _classes = classes;
            _bookings = bookings;
            _membershipTypes = membershipTypes;
            _memberService = memberService;
            _bookingService = bookingService;
            _authenticationService = authenticationService;
        }

        // MAIN MENU
public void Run()
{
    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine("        FITNESS CENTER DATABASE");
    Console.WriteLine("==========================================");
    Console.WriteLine();
    Console.WriteLine("LOGIN");
    Console.WriteLine("-----");
    Console.WriteLine();

    Console.Write("Username: ");
    string username = Console.ReadLine() ?? "";

    Console.Write("Password: ");
    string password = Console.ReadLine() ?? "";

    string? role = _authenticationService.Login(
        username,
        password);

    if (role == null)
    {
        Console.WriteLine();
        Console.WriteLine("Login failed.");
        Console.WriteLine("Invalid username or password.");
        Pause();
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Login successful.");
    Console.WriteLine($"Role: {role}");

    Pause();

    bool running = true;

    while (running)
    {
        Console.Clear();

        Console.WriteLine("==========================================");
        Console.WriteLine("        FITNESS CENTER DATABASE");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Logged in as: {username}");
        Console.WriteLine($"Role: {role}");
        Console.WriteLine("==========================================");
        Console.WriteLine();

        Console.WriteLine("1. Show database data");
        Console.WriteLine("2. Create new member");
        Console.WriteLine("3. Update member");

        if (role == "admin")
        {
            Console.WriteLine("4. Delete member");
        }

        Console.WriteLine("0. Exit");
        Console.WriteLine();

        Console.Write("Choose an option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowDatabaseData();
                Pause();
                break;

            case "2":
                CreateMember();
                Pause();
                break;

            case "3":
                UpdateMember();
                Pause();
                break;

            case "4":
                if (role == "admin")
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Delete functionality will be added next.");
                    Pause();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Access denied. Admin only.");
                    Pause();
                }
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine();
                Console.WriteLine("Invalid choice.");
                Pause();
                break;
        }
    }
}

        // CREATE NEW MEMBER
        private void CreateMember()
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("             CREATE MEMBER");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            Console.Write("First name: ");
            string firstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            string lastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Membership type ID: ");
            string membershipInput = Console.ReadLine() ?? "";

            if (!int.TryParse(
                membershipInput,
                out int membershipTypeId))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Invalid membership type ID. Please enter a number.");

                return;
            }

            try
            {
                int memberId = _memberService.CreateMember(
                    firstName,
                    lastName,
                    email,
                    membershipTypeId);

                Console.WriteLine();
                Console.WriteLine(
                    $"Member created successfully. Member ID: {memberId}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Validation error:");
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Operation could not be completed:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Database error:");
                Console.WriteLine(ex.Message);
            }
        }


        // ============================================================
// UPDATE MEMBER
// ============================================================
private void UpdateMember()
{
    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine("             UPDATE MEMBER");
    Console.WriteLine("==========================================");
    Console.WriteLine();

    Console.Write("Member ID: ");
    string memberIdInput = Console.ReadLine() ?? "";

    if (!int.TryParse(memberIdInput, out int memberId))
    {
        Console.WriteLine();
        Console.WriteLine("Invalid Member ID.");

        return;
    }

    Console.Write("First name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Last name: ");
    string lastName = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Membership type ID: ");
    string membershipInput = Console.ReadLine() ?? "";

    if (!int.TryParse(
        membershipInput,
        out int membershipTypeId))
    {
        Console.WriteLine();
        Console.WriteLine(
            "Invalid membership type ID.");

        return;
    }

    try
    {
        bool updated = _memberService.UpdateMember(
            memberId,
            firstName,
            lastName,
            email,
            membershipTypeId);

        Console.WriteLine();

        if (updated)
        {
            Console.WriteLine(
                "Member updated successfully.");
        }
        else
        {
            Console.WriteLine(
                "Member was not found.");
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine();
        Console.WriteLine("Validation error:");
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine("Database error:");
        Console.WriteLine(ex.Message);
    }
}

        // SHOW DATABASE DATA
        public void ShowDatabaseData()
        {
            Console.WriteLine("Tables in FitnessCenter:");
            Console.WriteLine();

            Console.WriteLine("- bookinghistory");
            Console.WriteLine("- bookings");
            Console.WriteLine("- classes");
            Console.WriteLine("- members");
            Console.WriteLine("- membershiptypes");
            Console.WriteLine("- trainers");

            Console.WriteLine();

            Console.WriteLine("==========================================");
            Console.WriteLine("              DATABASE DATA");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            ShowMembershipTypes();
            ShowMembers();
            ShowTrainers();
            ShowClasses();
            ShowBookings();
        }

        // SHOW MEMBERSHIP TYPES
        private void ShowMembershipTypes()
        {
            Console.WriteLine("MEMBERSHIP TYPES");
            Console.WriteLine("----------------");

            foreach (var type in _membershipTypes.GetAll())
            {
                Console.WriteLine(
                    $"{type.MembershipTypeID} | " +
                    $"{type.Name} | " +
                    $"{type.Price:0.00} kr | " +
                    $"{type.DurationMonths} month(s)");
            }

            Console.WriteLine();
        }

        // SHOW MEMBERS
        private void ShowMembers()
        {
            Console.WriteLine("MEMBERS");
            Console.WriteLine("-------");

            var types = _membershipTypes.GetAll()
                .ToDictionary(
                    x => x.MembershipTypeID,
                    x => x.Name);

            foreach (var member in _members.GetAll())
            {
                string membership =
                    types.TryGetValue(
                        member.MembershipTypeID,
                        out string? name)
                    ? name
                    : "Unknown";

                Console.WriteLine(
                    $"{member.MemberID} | " +
                    $"{member.FirstName} {member.LastName} | " +
                    $"{member.Email} | " +
                    $"{membership}");
            }

            Console.WriteLine();
        }

        // SHOW TRAINERS
        private void ShowTrainers()
        {
            Console.WriteLine("TRAINERS");
            Console.WriteLine("--------");

            foreach (var trainer in _trainers.GetAll())
            {
                Console.WriteLine(
                    $"{trainer.TrainerID} | " +
                    $"{trainer.FirstName} {trainer.LastName} | " +
                    $"{trainer.Specialization}");
            }

            Console.WriteLine();
        }

        // SHOW CLASSES
        private void ShowClasses()
        {
            Console.WriteLine("CLASSES");
            Console.WriteLine("-------");

            var trainers = _trainers.GetAll()
                .ToDictionary(
                    x => x.TrainerID,
                    x => $"{x.FirstName} {x.LastName}");

            foreach (var item in _classes.GetAll())
            {
                string trainer =
                    trainers.TryGetValue(
                        item.TrainerID,
                        out string? name)
                    ? name
                    : "Unknown";

                Console.WriteLine(
                    $"{item.ClassID} | " +
                    $"{item.Name} | " +
                    $"{item.StartTime:dd/MM/yyyy HH.mm.ss} | " +
                    $"Duration: {item.DurationMinutes} min | " +
                    $"Capacity: {item.Capacity} | " +
                    $"Trainer: {trainer}");
            }

            Console.WriteLine();
        }

        // SHOW BOOKINGS
        private void ShowBookings()
        {
            Console.WriteLine("BOOKINGS");
            Console.WriteLine("--------");

            var members = _members.GetAll()
                .ToDictionary(
                    x => x.MemberID,
                    x => $"{x.FirstName} {x.LastName}");

            var classes = _classes.GetAll()
                .ToDictionary(
                    x => x.ClassID,
                    x => x.Name);

            foreach (var booking in _bookings.GetAll())
            {
                string member =
                    members.TryGetValue(
                        booking.MemberID,
                        out string? memberName)
                    ? memberName
                    : "Unknown";

                string className =
                    classes.TryGetValue(
                        booking.ClassID,
                        out string? name)
                    ? name
                    : "Unknown";

                Console.WriteLine(
                    $"{booking.BookingID} | " +
                    $"Member: {member} | " +
                    $"Class: {className} | " +
                    $"Booked: {booking.BookingDate:dd/MM/yyyy HH.mm.ss}");
            }

            Console.WriteLine();
        }

        // PAUSE
        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}