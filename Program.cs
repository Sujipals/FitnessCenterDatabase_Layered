using FitnessCenterDatabase.Data;
using FitnessCenterDatabase.Presentation;
using FitnessCenterDatabase.Repositories;
using FitnessCenterDatabase.Services;

namespace FitnessCenterDatabase
{
    internal class Program
    {
        private const string ServerConnectionString =
            "Server=localhost;Port=3306;User ID=root;Password=MiAr@0625Pals;";

        private const string DatabaseConnectionString =
            "Server=localhost;Port=3306;Database=FitnessCenter;User ID=root;Password=MiAr@0625Pals;";


        private const string UserConnectionString =
            "Server=localhost;Port=3306;Database=FitnessCenter;User ID=fitness_user;Password=your-own-password;";

        private const string AdminConnectionString =
            "Server=localhost;Port=3306;Database=FitnessCenter;User ID=admin;Password=admin-password;";

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("       FITNESS CENTER DATABASE");
            Console.WriteLine("          LAYERED ARCHITECTURE");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            try
            {
                // Create database if it does not already exist.
                DatabaseInitializer initializer =
                    new DatabaseInitializer(ServerConnectionString);

                initializer.Initialize();

                // Create database connection.
                DatabaseConnection databaseConnection =
                    new DatabaseConnection(DatabaseConnectionString);

                // Create repositories.
                MemberRepository memberRepository =
                    new MemberRepository(databaseConnection);

                TrainerRepository trainerRepository =
                    new TrainerRepository(databaseConnection);

                ClassRepository classRepository =
                    new ClassRepository(databaseConnection);

                BookingRepository bookingRepository =
                    new BookingRepository(databaseConnection);

                MembershipTypeRepository membershipTypeRepository =
                    new MembershipTypeRepository(databaseConnection);

                // Create services.
                MemberService memberService =
                    new MemberService(memberRepository);

                AuthenticationRepository authenticationRepository =
                    new AuthenticationRepository(databaseConnection);

                AuthenticationService authenticationService =
                     new AuthenticationService(authenticationRepository);

                BookingService bookingService =
                    new BookingService(
                        bookingRepository,
                        memberRepository,
                        classRepository);

                BusinessRuleService businessRuleService =
                    new BusinessRuleService(DatabaseConnectionString);

                SeedService seedService =
                    new SeedService(DatabaseConnectionString);

                TriggerService triggerService =
                    new TriggerService(DatabaseConnectionString);

                // Create log file path.
                string logFilePath =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Logs",
                        "database.log");

                LogService logService =
                    new LogService(logFilePath);

                // Seed database with test data.
                seedService.Seed();

                // Test business rules.
                businessRuleService.TestRules();

                // Create database trigger.
                triggerService.CreateBookingInsertTrigger();

                // Test booking and logging.
                bookingService.TestBookingAndLogging(logService);

                // Create Console UI.
                ConsoleUI ui =
                    new ConsoleUI(
                        memberRepository,
                        trainerRepository,
                        classRepository,
                        bookingRepository,
                        membershipTypeRepository,
                        memberService,
                        bookingService,
                        authenticationService);

                // Start the menu.
                ui.Run();

                Console.WriteLine();
                Console.WriteLine("==========================================");
                Console.WriteLine("FitnessCenter application closed.");
                Console.WriteLine("==========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("==========================================");
                Console.WriteLine("An error occurred:");
                Console.WriteLine("==========================================");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }
        }
    }
}