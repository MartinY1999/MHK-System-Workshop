using MHKDTO;
using MHKDTO.Models;
using MHKDTO.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHKFrontEnd.Services.Interfaces
{
    public interface IAPIClient
    {
        Task<IEnumerable<FinanceResponse>> GetFinancesAsync();
        Task<FinanceResponse> GetFinanceAsync(int id);
        Task<bool> AddFinanceAsync(Finance finance);
        Task PutFinanceAsync(Finance finance);
        Task DeleteFinanceAsync(int id);


        Task<IEnumerable<ProjectResponse>> GetProjectsAsync();
        Task<ProjectResponse> GetProjectAsync(int id);
        Task<bool> AddProjectAsync(Project project);
        Task PutProjectAsync(Project project);
        Task DeleteProjectAsync(int id);

        Task<IEnumerable<SaleResponse>> GetSalesAsync();
        Task<SaleResponse> GetSaleAsync(int id);
        Task<bool> AddSaleAsync(Sale sale);
        Task PutSaleAsync(Sale sale);
        Task DeleteSaleAsync(int id);

        Task<IEnumerable<SupportResponse>> GetSupportsAsync();
        Task<SupportResponse> GetSupportAsync(int id);
        Task<bool> AddSupportAsync(Support support);
        Task PutSupportAsync(Support support);
        Task DeleteSupportAsync(int id);

        Task<List<SearchResult>> SearchAsync(string query);
    }
}
