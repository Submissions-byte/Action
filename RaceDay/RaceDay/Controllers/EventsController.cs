using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using RaceDay.Database;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly RaceDayDBContext _context;
    public EventsController(RaceDayDBContext context)
    {
        _context = context;
    }

    // GET: api/Event
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
    {
        return await _context.Event.ToListAsync();
    }

    // GET: api/Event/5
    [HttpGet("{eventid}")]
    public async Task<ActionResult<Event>> GetEvent(int eventid)
    {
        var event = await _context.Event.FindAsync(eventid);

        if (event == null)
        {
            return NotFound();
        }

        return event;
    }

    // PUT: api/Event/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{eventid}")]
    public async Task<IActionResult> PutEvent(int? eventid, Event event)
    {
        if (eventid != event.EventID)
        {
            return BadRequest();
        }

        _context.Entry(event).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventExists(eventid))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Event
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event event)
    {
        _context.Event.Add(event);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEvent", new { eventid = event.EventID }, event);
    }

    // DELETE: api/Event/5
    [HttpDelete("{eventid}")]
    public async Task<IActionResult> DeleteEvent(int? eventid)
    {
        var event = await _context.Event.FindAsync(eventid);
        if (event == null)
        {
            return NotFound();
        }

        _context.Event.Remove(event);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(int? eventid)
    {
        return _context.Event.Any(e => e.EventID == eventid);
    }
}
