using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using RaceDay.Database;
using Results = RaceDay.Models.Results;

[Route("api/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
    private readonly RaceDayDBContext _context;
    public ResultsController(RaceDayDBContext context)
    {
        _context = context;
    }

    // GET: api/Results
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Results>>> GetResults()
    {
        return await _context.Results.ToListAsync();
    }

    // GET: api/Results/5
    [HttpGet("{resultid}")]
    public async Task<ActionResult<Results>> GetResults(int resultid)
    {
        var results = await _context.Results.FindAsync(resultid);

        if (results == null)
        {
            return NotFound();
        }

        return results;
    }

    // PUT: api/Results/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{resultid}")]
    public async Task<IActionResult> PutResults(int? resultid, Results results)
    {
        if (resultid != results.ResultID)
        {
            return BadRequest();
        }

        _context.Entry(results).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ResultsExists(resultid))
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

    // POST: api/Results
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Results>> PostResults(Results results)
    {
        _context.Results.Add(results);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetResults", new { resultid = results.ResultID }, results);
    }

    // DELETE: api/Results/5
    [HttpDelete("{resultid}")]
    public async Task<IActionResult> DeleteResults(int? resultid)
    {
        var results = await _context.Results.FindAsync(resultid);
        if (results == null)
        {
            return NotFound();
        }

        _context.Results.Remove(results);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ResultsExists(int? resultid)
    {
        return _context.Results.Any(e => e.ResultID == resultid);
    }
}
