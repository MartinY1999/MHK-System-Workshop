using MHKDTO.Models;
using MHKDTO.Responses;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    //TODO: Implement EditProject, EditSale, EditSupport classes and functionality
    public class EditFinanceModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public EditFinanceModel(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Finance Finance { get; set; }

        public async Task OnGetAsync(int id)
        {
            FinanceResponse finance = await _apiClient.GetFinanceAsync(id);

            this.Finance = new Finance
            {
                Id = finance.Id,
                BSPE = finance.BSPE,
                Minimum = finance.Minimum,
                Maximum = finance.Maximum,
                Month = finance.Month,
                Year = finance.Year,
                MonthlySum = finance.MonthlySum,
                PeopleCount = finance.PeopleCount
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _apiClient.PutFinanceAsync(this.Finance);
            return RedirectToPage("/FinancePage", new { id = this.Finance.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await _apiClient.GetFinanceAsync(id);

            if (session != null)
            {
                await _apiClient.DeleteFinanceAsync(id);
            }

            return Page();
        }
    }
}