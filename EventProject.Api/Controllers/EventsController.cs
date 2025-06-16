using EventProject.Core;
using Microsoft.AspNetCore.Mvc;

namespace EventProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ITicketService _ticketService;

    public EventsController(IEventService eventService, ITicketService ticketService)
    {
        _eventService = eventService;
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUpcomingEvents([FromQuery] int days = 30)
    {
        // Validate the input
        if (days != 30 && days != 60 && days != 180)
        {
            return BadRequest("Please provide a valid day range: 30, 60, or 180.");
        }
        var events = await _eventService.GetUpcomingEvents(days);
        return Ok(events);
    }

    [HttpGet("sales-summary/by-count")]
    public async Task<IActionResult> GetTopSalesByCount()
    {
        var summary = await _ticketService.GetTop5EventsBySalesCount();
        return Ok(summary);
    }

    [HttpGet("sales-summary/by-value")]
    public async Task<IActionResult> GetTopSalesByValue()
    {
        var summary = await _ticketService.GetTop5EventsBySalesValue();
        return Ok(summary);
    }
}