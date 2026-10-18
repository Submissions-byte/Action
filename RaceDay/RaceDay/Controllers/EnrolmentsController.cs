using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using RaceDay.Database;

[Route("api/[controller]")]
[ApiController]
public class EnrolmentsController : ControllerBase
{
    private readonly RaceDayDBContext _context;
    public EnrolmentsController(RaceDayDBContext context)
    {
        _context = context;
    }

    // GET: api/Enrolment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolment()
    {
        return await _context.Enrolment.ToListAsync();
    }

    // GET: api/Enrolment/5
    [HttpGet("{enrolmentid}")]
    public async Task<ActionResult<Enrolment>> GetEnrolment(int enrolmentid)
    {
        var enrolment = await _context.Enrolment.FindAsync(enrolmentid);

        if (enrolment == null)
        {
            return NotFound();
        }

        return enrolment;
    }

    // PUT: api/Enrolment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{enrolmentid}")]
    public async Task<IActionResult> PutEnrolment(int? enrolmentid, Enrolment enrolment)
    {
        if (enrolmentid != enrolment.EnrolmentID)
        {
            return BadRequest();
        }

        _context.Entry(enrolment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnrolmentExists(enrolmentid))
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

    // POST: api/Enrolment
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Enrolment>> PostEnrolment(Enrolment enrolment)
    {
        _context.Enrolment.Add(enrolment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEnrolment", new { enrolmentid = enrolment.EnrolmentID }, enrolment);
    }

    // DELETE: api/Enrolment/5
    [HttpDelete("{enrolmentid}")]
    public async Task<IActionResult> DeleteEnrolment(int? enrolmentid)
    {
        var enrolment = await _context.Enrolment.FindAsync(enrolmentid);
        if (enrolment == null)
        {
            return NotFound();
        }

        _context.Enrolment.Remove(enrolment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EnrolmentExists(int? enrolmentid)
    {
        return _context.Enrolment.Any(e => e.EnrolmentID == enrolmentid);
    }
}
