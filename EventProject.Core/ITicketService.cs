using EventProject.Core.Entities;

namespace EventProject.Core;

// DTO for sales summary data
public record EventSalesSummary(string EventId, string EventName, long SalesValue);

public interface ITicketService
{
    Task<IEnumerable<TicketSales>> GetTicketsForEvent(string eventId);
    Task<IEnumerable<EventSalesSummary>> GetTop5EventsBySalesCount();
    Task<IEnumerable<EventSalesSummary>> GetTop5EventsBySalesValue();
}