namespace TicketBookingSystem
{
    internal class Ticket
    {
        private static int ticketIdCounter = 0;
        public Ticket(int? id, string eventName, TicketType type, float price)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName), "Event name cannot be null or empty.");

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

            ticketId = id ?? ++ticketIdCounter; // Use provided ID or auto-generate
            this.eventName = eventName;
            this.type = type;
            this.price = price;
        }

        public int ticketId { get; } // Read-only property.
        public string eventName { get; }
        public TicketType type { get; }
        public float price { get; }

        public override string ToString() => $"Ticket ID: {ticketId}, Event: {eventName}, Type: {type}, Price: {price:C}";
    }
}
