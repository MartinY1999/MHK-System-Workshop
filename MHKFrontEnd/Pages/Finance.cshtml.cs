﻿using MHKDTO.Responses;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MHKFrontEnd
{
    public class FinanceModel : PageModel
    {
        private readonly IAPIClient _apiClient;

        public FinanceModel(IAPIClient apiClient)
        {
            this._apiClient = apiClient;
        }

        public IEnumerable<IGrouping<int, FinanceResponse>> Finances { get; set; }
        public int CurrentYear { get; set; }
        public IEnumerable<(int Offset, string Month)> MonthOffsets { get; set; }
        public int CurrentMonthOffset { get; set; }

        public async Task OnGetAsync(int month = 0)
        {
            this.CurrentMonthOffset = month;
            this.CurrentYear = DateTime.UtcNow.Year;

            var finances = await _apiClient.GetFinancesAsync();

            this.MonthOffsets = finances.Select(f => f.Month)
                                        .OrderBy(m => m)
                                        .Select(month => (month, DateTimeFormatInfo.CurrentInfo.GetMonthName(month)));

            this.Finances = finances.OrderBy(f => f.Id)
                                    .GroupBy(f => f.Month)
                                    .OrderBy(g => g.Key);
        }
    }
}