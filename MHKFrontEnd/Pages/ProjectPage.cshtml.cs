using MHKDTO.Responses;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    public class ProjectPageModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public ProjectPageModel(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }

        public ProjectResponse Project { get; set; }
        public string CurrentMonth { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            this.Project = await _apiClient.GetProjectAsync(id);
            if (this.Project == null)
                return RedirectToPage("/Project");

            this.CurrentMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(this.Project.Month);

            return Page();
        }
    }
}