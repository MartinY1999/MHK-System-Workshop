using MHKBackend.Data;
using MHKBackend.Infrastructure;
using MHKDTO;
using MHKDTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHKBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SearchController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<SearchResult>>> Search(SearchTerm term)
        {
            string[] query = term.Query.Split(' ');
            if (query == null || !int.TryParse(query[0], out int month) || !int.TryParse(query[1], out int year))
                return NotFound(term);

            IList<Data.Models.Finance> financeResults = await this.context.Finances.Where(f => f.Month == Convert.ToInt32(query[0])
                                                                   && f.Year == Convert.ToInt32(query[1]))
                                                            .ToListAsync();
            IList<Data.Models.Project> projectResults = await this.context.Projects.Where(p => p.Month == Convert.ToInt32(query[0])
                                                                   && p.Year == Convert.ToInt32(query[1]))
                                                            .ToListAsync();
            IList<Data.Models.Sale> saleResults = await this.context.Sales.Where(s => s.Month == Convert.ToInt32(query[0])
                                                                   && s.Year == Convert.ToInt32(query[1]))
                                                            .ToListAsync();
            IList<Data.Models.Support> supportResults = await this.context.Supports.Where(s => s.Month == Convert.ToInt32(query[0])
                                                                   && s.Year == Convert.ToInt32(query[1]))
                                                            .ToListAsync();

            List<SearchResult> results = financeResults
                .Select(f => new SearchResult
            {
                Type = SearchResultType.Finance,
                Finance = f.MapFinanceResponse()
            })
            .Concat(projectResults
            .   Select(p => new SearchResult
            {
                Type = SearchResultType.Project,
                Project = p.MapProjectResponse()
            }))
            .Concat(saleResults
                .Select(s => new SearchResult
            {
                Type = SearchResultType.Sale,
                Sale = s.MapSaleResponse()
            }))
            .Concat(supportResults
                .Select(s => new SearchResult
            {
                Type = SearchResultType.Support,
                Support = s.MapSupportResponse()
            })).ToList();

            return results;
        }
    }
}