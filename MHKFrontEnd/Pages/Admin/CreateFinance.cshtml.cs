using MHKDTO.Models;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    //TODO: Implement CreateProject, CreateSale, CreateSupport classes and functionality
    public class CreateFinanceModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public CreateFinanceModel(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Finance Finance { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _apiClient.PutFinanceAsync(this.Finance);
            return RedirectToPage("/FinancePage", new { id = this.Finance.Id });
        }
    }
}