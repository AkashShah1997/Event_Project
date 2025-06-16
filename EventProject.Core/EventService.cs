using EventProject.Core.Entities;
using NHibernate;
using NHibernate.Linq;

namespace EventProject.Core;

public class EventService : IEventService
{
    private readonly NHibernate.ISession _session;

    public EventService(NHibernate.ISession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<Events>> GetUpcomingEvents(int days)
    {
        var now = DateTime.UtcNow;
        var futureDate = now.AddDays(days);

        // Fetch events within the specified date range 
        return await _session.Query<Events>()
                             .Where(e => e.StartsOn >= now && e.StartsOn <= futureDate)
                             .OrderBy(e => e.StartsOn)
                             .ToListAsync();
    }
}