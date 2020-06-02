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
    public class FinancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Finances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanceResponse>>> GetFinances()
        {
            return await _context.Finances
                .AsNoTracking()
                .Select(finance => finance.MapFinanceResponse())
                .ToListAsync();
        }

        // GET: api/Finances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinanceResponse>> GetFinance(int id)
        {
            var finance = await _context.Finances.FindAsync(id);

            if (finance == null)
            {
                return NotFound();
            }

            return finance.MapFinanceResponse();
        }

        // PUT: api/Finances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinance(int id, MHKDTO.Models.Finance input)
        {
            var finance = await _context.Finances.FindAsync(id);

            if (finance == null)
            {
                return NotFound();
            }

            finance.BSPE = input.BSPE;
            finance.Minimum = input.Minimum;
            finance.Maximum = input.Maximum;
            finance.MonthlySum = input.MonthlySum;
            finance.PeopleCount = input.PeopleCount;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Finances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<FinanceResponse>> PostFinance(MHKDTO.Models.Finance input)
        {
            // Check if the finance object already exists
            var existingFinanceObject = await _context.Finances
                .Where(f => f.Month == input.Month &&
                            f.Year == input.Year)
                .FirstOrDefaultAsync();

            if (existingFinanceObject != null)
            {
                return Conflict(input);
            }

            var finance = new Data.Models.Finance
            {
                BSPE = input.BSPE,
                Minimum = input.Minimum,
                Maximum = input.Maximum,
                Month = input.Month,
                Year = input.Year,
                MonthlySum = input.MonthlySum,
                PeopleCount = input.PeopleCount
            };

            _context.Finances.Add(finance);
            await _context.SaveChangesAsync();

            var result = finance.MapFinanceResponse();

            return CreatedAtAction(nameof(GetFinance), new { id = result.Id }, 
            result);
        }

        // DELETE: api/Finances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinance(int id)
        {
            var finance = await _context.Finances.FindAsync(id);
            if (finance == null)
            {
                return NotFound();
            }

            _context.Finances.Remove(finance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinanceExists(int id)
        {
            return _context.Finances.Any(e => e.Id == id);
        }
    }
}
