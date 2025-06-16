using EventProject.Core;
using EventProject.Core.Entities;
using NHibernate;
using NHibernate.Linq;

namespace EventProject.Core;

public class TicketService : ITicketService
{
    private readonly NHibernate.ISession _session;

    public TicketService(NHibernate.ISession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<TicketSales>> GetTicketsForEvent(string eventId)
    {
        return await _session.Query<TicketSales>()
                             .Where(t => t.Event.Id == eventId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<EventSalesSummary>> GetTop5EventsBySalesCount()
    {
        // Query to get top 5 events by ticket count 
        return await _session.Query<TicketSales>()
            .GroupBy(t => t.Event)
            .Select(g => new EventSalesSummary(g.Key.Id, g.Key.Name, g.Count()))
            .OrderByDescending(s => s.SalesValue)
            .Take(5)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventSalesSummary>> GetTop5EventsBySalesValue()
    {
        // Query to get top 5 events by total sales amount 
        return await _session.Query<TicketSales>()
            .GroupBy(t => t.Event)
            .Select(g => new EventSalesSummary(g.Key.Id, g.Key.Name, (long)g.Sum(t => t.PriceInCents)))
            .OrderByDescending(s => s.SalesValue)
            .Take(5)
            .ToListAsync();
    }
}