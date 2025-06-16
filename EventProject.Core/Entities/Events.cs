namespace EventProject.Core.Entities
{
    public class Events
    {
        public virtual string Id { get; set; } // Changed to string
        public virtual string Name { get; set; }
        public virtual DateTime StartsOn { get; set; }
        public virtual DateTime EndsOn { get; set; }
        public virtual string Location { get; set; }

        // Add this for the one-to-many relationship
        public virtual ISet<TicketSales> TicketSales { get; set; } = new HashSet<TicketSales>();
    }
}