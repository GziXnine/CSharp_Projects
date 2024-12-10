namespace TicketBookingSystem
{
    internal interface ITicketBooking
    {
        void BookTicket(Ticket ticket);
        void CancelTicket(int ticketId);
        void DisplayAvailableTickets();
        void DisplayBookedTickets();
    }

    internal class BookingSystem : ITicketBooking
    {
        internal readonly List<Ticket> availableTickets = new List<Ticket>();
        private readonly List<Ticket> bookedTickets = new List<Ticket>();

        public event TicketBookedHandler TicketBooked = delegate { };

        public void AddTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

            if (availableTickets.Any(t => t.ticketId == ticket.ticketId) || bookedTickets.Any(t => t.ticketId == ticket.ticketId))
            {
                Console.WriteLine($"Ticket with ID {ticket.ticketId} already exists.");
                return;
            }

            availableTickets.Add(ticket);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ticket {ticket.ticketId} added successfully.");
            Console.ResetColor();
        }

        public void BookTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

            if (availableTickets.Remove(ticket))
            {
                bookedTickets.Add(ticket);
                TicketBooked?.Invoke(ticket); // Notify via delegate
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Ticket {ticket.ticketId} booked successfully.");
                Console.ResetColor();
            }
            else
                Console.WriteLine($"Ticket {ticket.ticketId} is not available.");
        }

        public void CancelTicket(int ticketId)
        {
            var ticket = bookedTickets.FirstOrDefault(t => t.ticketId == ticketId); // a LINQ Query

            if (ticket != null)
            {
                bookedTickets.Remove(ticket);
                availableTickets.Add(ticket);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Ticket {ticketId} has been canceled.");
                Console.ResetColor();
            }
            else
                Console.WriteLine($"Ticket {ticketId} not found.");
        }

        public void DisplayAvailableTickets() => DisplayTickets("Available", availableTickets);

        public void DisplayBookedTickets() => DisplayTickets("Booked", bookedTickets);

        private void DisplayTickets(string type, List<Ticket> tickets)
        {
            Console.WriteLine(type + " Tickets:");

            if(type == "Available")
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            if (!tickets.Any())
                Console.WriteLine($"No tickets {type}.");
            else
                tickets.ForEach(ticket => Console.WriteLine(ticket));

            Console.ResetColor();
        }
    }
}
