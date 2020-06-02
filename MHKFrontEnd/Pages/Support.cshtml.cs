using MHKDTO.Responses;
using MHKFrontEnd.Services;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    public class SupportModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public SupportModel(IAPIClient apiClient)
        {
            this._apiClient = apiClient;
        }

        public IEnumerable<IGrouping<int, SupportResponse>> Supports { get; set; }
        public int CurrentYear { get; set; }
        public IEnumerable<(int Offset, string Month)> MonthOffsets { get; set; }
        public int CurrentMonthOffset { get; set; }

        public async Task OnGetAsync(int month = 0)
        {
            this.CurrentMonthOffset = month;
            this.CurrentYear = DateTime.UtcNow.Year;

            var supports = await _apiClient.GetSupportsAsync();

            this.MonthOffsets = supports.Select(f => f.Month)
                                        .OrderBy(m => m)
                                        .Select(month => (month, DateTimeFormatInfo.CurrentInfo.GetMonthName(month)));

            this.Supports = supports.OrderBy(f => f.Id)
                                    .GroupBy(f => f.Month)
                                    .OrderBy(g => g.Key);
        }
    }
}