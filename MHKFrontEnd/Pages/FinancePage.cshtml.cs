using MHKDTO.Responses;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    //TODO: Update ProjectPage, SalePage, SupportPage
    //TODO: On button click "Back" should return to previous page
    public class FinancePageModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public FinancePageModel(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }

        public bool IsAdmin { get; set; }
        public FinanceResponse Finance { get; set; }
        public string CurrentMonth { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            IsAdmin = User.IsAdmin();

            this.Finance = await _apiClient.GetFinanceAsync(id);
            if (this.Finance == null)
                 return RedirectToPage("/Finance");

            this.CurrentMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(this.Finance.Month);

            return Page();
        }
    }
}