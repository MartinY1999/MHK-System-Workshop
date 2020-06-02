using MHKBackend.Data;
using MHKBackend.Data.Models;
using MHKBackend.Infrastructure;
using MHKDTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHKBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SupportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Supports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportResponse>>> GetSupports()
        {
            return await _context.Supports
                .AsNoTracking()
                .Select(s => s.MapSupportResponse())
                .ToListAsync();
        }

        // GET: api/Supports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupportResponse>> GetSupport(int id)
        {
            var support = await _context.Supports.FindAsync(id);

            if (support == null)
            {
                return NotFound();
            }

            return support.MapSupportResponse();
        }

        // PUT: api/Supports/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupport(int id, MHKDTO.Models.Support input)
        {
            var support = await _context.Supports.FindAsync(id);

            if (support == null)
            {
                return NotFound();
            }

            support.WeightNK = input.WeightNK;
            support.Minimum = input.Minimum;
            support.Maximum = input.Maximum;
            support.TotalTasks = input.TotalTasks;
            support.TasksOnTime = input.TasksOnTime;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Supports
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<SupportResponse>> PostSupport(MHKDTO.Models.Support input)
        {
            // Check if the support object already exists
            var existingSupportObject = await _context.Supports
                .Where(s => s.Month == input.Month &&
                            s.Year == input.Year)
                .FirstOrDefaultAsync();

            if (existingSupportObject != null)
            {
                return Conflict(input);
            }

            var support = new Data.Models.Support
            {
                WeightNK = input.WeightNK,
                Minimum = input.Minimum,
                Maximum = input.Maximum,
                Month = input.Month,
                Year = input.Year,
                TotalTasks = input.TotalTasks,
                TasksOnTime = input.TasksOnTime
            };

            _context.Supports.Add(support);
            await _context.SaveChangesAsync();

            var result = support.MapSupportResponse();

            return CreatedAtAction(nameof(GetSupport), new { id = result.Id },
            result);
        }

        // DELETE: api/Supports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupport(int id)
        {
            var support = await _context.Supports.FindAsync(id);
            if (support == null)
            {
                return NotFound();
            }

            _context.Supports.Remove(support);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupportExists(int id)
        {
            return _context.Supports.Any(e => e.Id == id);
        }
    }
}
