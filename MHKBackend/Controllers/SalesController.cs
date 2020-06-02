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
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleResponse>>> GetSales()
        {
            return await _context.Sales
                .AsNoTracking()
                .Select(s => s.MapSaleResponse())
                .ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleResponse>> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale.MapSaleResponse();
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, MHKDTO.Models.Sale input)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            sale.WeightNK = input.WeightNK;
            sale.Minimum = input.Minimum;
            sale.Maximum = input.Maximum;
            sale.TotalTasks = input.TotalTasks;
            sale.TasksOnTime = input.TasksOnTime;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Sales
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<SaleResponse>> PostSale(MHKDTO.Models.Sale input)
        {
            // Check if the sale object already exists
            var existingSaleObject = await _context.Sales
                .Where(s => s.Month == input.Month &&
                            s.Year == input.Year)
                .FirstOrDefaultAsync();

            if (existingSaleObject != null)
            {
                return Conflict(input);
            }

            var sale = new Data.Models.Sale
            {
                WeightNK = input.WeightNK,
                Minimum = input.Minimum,
                Maximum = input.Maximum,
                Month = input.Month,
                Year = input.Year,
                TotalTasks = input.TotalTasks,
                TasksOnTime = input.TasksOnTime
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var result = sale.MapSaleResponse();

            return CreatedAtAction(nameof(GetSale), new{ id = result.Id },
            result);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
