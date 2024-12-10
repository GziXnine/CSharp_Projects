namespace TicketBookingSystem
{
    internal delegate void TicketBookedHandler(Ticket ticket);

    internal enum TicketType
    {
        Regular,
        VIP,
        Child
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            var bookingSystem = new BookingSystem();
            bool exit = false;
            Ticket[] tickets = new Ticket[]
            {
                new Ticket(null, "Rock Concert", TicketType.Regular, 50.0f),
                new Ticket(null, "VIP Dinner", TicketType.VIP, 150.0f),
                new Ticket(null, "Child Play", TicketType.Child, 20.0f),
                new Ticket(null, "Football Match", TicketType.Regular, 60.0f),
                new Ticket(null, "Music Festival", TicketType.VIP, 120.0f)
            };

            foreach (Ticket ticket in tickets)
            {
                bookingSystem.AddTicket(ticket);
            }

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Ticket Booking System ===");
                Console.WriteLine("1. Add Ticket");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Cancel Ticket");
                Console.WriteLine("4. Display Available Tickets");
                Console.WriteLine("5. Display Booked Tickets");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTicketUI(bookingSystem);
                        break;

                    case "2":
                        BookTicketUI(bookingSystem);
                        break;

                    case "3":
                        CancelTicketUI(bookingSystem);
                        break;

                    case "4":
                        bookingSystem.DisplayAvailableTickets();
                        Pause();
                        break;

                    case "5":
                        bookingSystem.DisplayBookedTickets();
                        Pause();
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private static void AddTicketUI(BookingSystem bookingSystem)
        {
            Console.Clear();
            Console.WriteLine("--- Add Ticket ---");

            Console.Write("Enter Event Name: ");
            var eventName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Ticket Type (0=Regular, 1=VIP, 2=Child): ");
            if (!Enum.TryParse(Console.ReadLine(), out TicketType type))
            {
                Console.WriteLine("Invalid ticket type.");
                Pause();
                return;
            }

            Console.Write("Enter Ticket Price: ");
            if (!float.TryParse(Console.ReadLine(), out var price) || price <= 0)
            {
                Console.WriteLine("Invalid price.");
                Pause();
                return;
            }

            Console.Write("Enter Custom Ticket ID (or leave blank for auto-generated): ");
            var idInput = Console.ReadLine();
            int? customId = string.IsNullOrWhiteSpace(idInput) ? null : int.Parse(idInput);

            var ticket = new Ticket(customId, eventName, type, price);
            bookingSystem.AddTicket(ticket);

            Pause();
        }

        private static void BookTicketUI(BookingSystem bookingSystem)
        {
            Console.Clear();
            Console.WriteLine("--- Book Ticket ---");

            Console.Write("Enter Ticket ID to Book: ");
            if (!int.TryParse(Console.ReadLine(), out var ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                Pause();
                return;
            }

            var ticket = bookingSystem.availableTickets.FirstOrDefault(t => t.ticketId == ticketId);
            if (ticket != null)
                bookingSystem.BookTicket(ticket);
            else
                Console.WriteLine($"Ticket {ticketId} not found.");

            Pause();
        }

        private static void CancelTicketUI(BookingSystem bookingSystem)
        {
            Console.Clear();
            Console.WriteLine("--- Cancel Ticket ---");

            Console.Write("Enter Ticket ID to Cancel: ");
            if (!int.TryParse(Console.ReadLine(), out var ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                Pause();
                return;
            }

            bookingSystem.CancelTicket(ticketId);
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
