using System;

namespace EventProject.Core.Entities
{
    public class TicketSales
    {
        public virtual string Id { get; set; } // Changed to string
        public virtual string EventId { get; set; } // Matches DB foreign key type (TEXT)
        public virtual string UserId { get; set; }
        public virtual DateTime PurchaseDate { get; set; }
        public virtual decimal PriceInCents { get; set; } // INTEGER in DB maps to int or long, but decimal is often preferred for currency. If you must use int/long, be aware of precision.
        public virtual Events Event { get; set; } // Many-to-one relationship
    }
}