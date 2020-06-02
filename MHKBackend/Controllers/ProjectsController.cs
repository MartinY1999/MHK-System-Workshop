using MHKBackend.Data;
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
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetProjects()
        {
            return await _context.Projects
                .AsNoTracking()
                .Select(p => p.MapProjectResponse())
                .ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponse>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project.MapProjectResponse();
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, MHKDTO.Models.Project input)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.WeightNK = input.WeightNK;
            project.Minimum = input.Minimum;
            project.Maximum = input.Maximum;
            project.TotalTasks = input.TotalTasks;
            project.TasksOnTime = input.TasksOnTime;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProjectResponse>> PostProject(MHKDTO.Models.Project input)
        {
            // Check if the project object already exists
            var existingProjectObject = await _context.Projects
                .Where(p => p.Month == input.Month &&
                            p.Year == input.Year)
                .FirstOrDefaultAsync();

            if (existingProjectObject != null)
            {
                return Conflict(input);
            }

            var project = new Data.Models.Project
            {
                WeightNK = input.WeightNK,
                Minimum = input.Minimum,
                Maximum = input.Maximum,
                Month = input.Month,
                Year = input.Year,
                TotalTasks = input.TotalTasks,
                TasksOnTime = input.TasksOnTime
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var result = project.MapProjectResponse();

            return CreatedAtAction(nameof(GetProject), new { id = result.Id },
            result);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
