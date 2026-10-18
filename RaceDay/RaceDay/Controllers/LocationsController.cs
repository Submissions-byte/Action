using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using RaceDay.Database;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly RaceDayDBContext _context;
    public LocationsController(RaceDayDBContext context)
    {
        _context = context;
    }

    // GET: api/Location
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
    {
        return await _context.Location.ToListAsync();
    }

    // GET: api/Location/5
    [HttpGet("{locationid}")]
    public async Task<ActionResult<Location>> GetLocation(int locationid)
    {
        var location = await _context.Location.FindAsync(locationid);

        if (location == null)
        {
            return NotFound();
        }

        return location;
    }

    // PUT: api/Location/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{locationid}")]
    public async Task<IActionResult> PutLocation(int? locationid, Location location)
    {
        if (locationid != location.LocationID)
        {
            return BadRequest();
        }

        _context.Entry(location).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LocationExists(locationid))
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

    // POST: api/Location
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Location>> PostLocation(Location location)
    {
        _context.Location.Add(location);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLocation", new { locationid = location.LocationID }, location);
    }

    // DELETE: api/Location/5
    [HttpDelete("{locationid}")]
    public async Task<IActionResult> DeleteLocation(int? locationid)
    {
        var location = await _context.Location.FindAsync(locationid);
        if (location == null)
        {
            return NotFound();
        }

        _context.Location.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LocationExists(int? locationid)
    {
        return _context.Location.Any(e => e.LocationID == locationid);
    }
}
