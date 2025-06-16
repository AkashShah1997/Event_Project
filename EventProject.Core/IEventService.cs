using EventProject.Core.Entities;

namespace EventProject.Core;

public interface IEventService
{
    Task<IEnumerable<Events>> GetUpcomingEvents(int days);
}