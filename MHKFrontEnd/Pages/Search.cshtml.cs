using MHKDTO;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    public class SearchModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public SearchModel(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }

        public string Term { get; set; }

        public List<SearchResult> SearchResults { get; set; }

        public async Task OnGetAsync(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                this.SearchResults = new List<SearchResult>();
                return;
            }

            Term = term;
            SearchResults = await _apiClient.SearchAsync(term);
        }
    }
}