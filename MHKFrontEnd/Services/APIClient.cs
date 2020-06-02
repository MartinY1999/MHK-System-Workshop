using MHKDTO;
using MHKDTO.Models;
using MHKDTO.Responses;
using MHKFrontEnd.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MHKFrontEnd.Services
{
    public class APIClient : IAPIClient
    {
        private readonly HttpClient httpClient;

        public APIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> AddFinanceAsync(Finance finance)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/finances", finance);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> AddProjectAsync(Project project)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/projects", project);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> AddSaleAsync(Sale sale)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/sales", sale);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> AddSupportAsync(Support support)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/supports", support);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<FinanceResponse> GetFinanceAsync(int id)
        {
            var response = await httpClient.GetAsync($"/api/finances/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<FinanceResponse>();
        }

        public async Task<IEnumerable<FinanceResponse>> GetFinancesAsync()
        {
            var response = await httpClient.GetAsync("/api/Finances");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<FinanceResponse>>();
        }

        public async Task<ProjectResponse> GetProjectAsync(int id)
        {
            var response = await httpClient.GetAsync($"/api/projects/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProjectResponse>();
        }

        public async Task<IEnumerable<ProjectResponse>> GetProjectsAsync()
        {
            var response = await httpClient.GetAsync("/api/projects");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<ProjectResponse>>();
        }

        public async Task<SaleResponse> GetSaleAsync(int id)
        {
            var response = await httpClient.GetAsync($"/api/sales/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<SaleResponse>();
        }

        public async Task<IEnumerable<SaleResponse>> GetSalesAsync()
        {
            var response = await httpClient.GetAsync("/api/sales");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<SaleResponse>>();
        }

        public async Task<SupportResponse> GetSupportAsync(int id)
        {
            var response = await httpClient.GetAsync($"/api/supports/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<SupportResponse>();
        }

        public async Task<IEnumerable<SupportResponse>> GetSupportsAsync()
        {
            var response = await httpClient.GetAsync("/api/supports");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<SupportResponse>>();
        }

        public async Task DeleteFinanceAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/finances/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/projects/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSaleAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/sales/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSupportAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/supports/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task PutFinanceAsync(Finance finance)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/finances/{finance.Id}", finance);

            response.EnsureSuccessStatusCode();
        }

        public async Task PutProjectAsync(Project project)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/projects/{project.Id}", project);

            response.EnsureSuccessStatusCode();
        }

        public async Task PutSaleAsync(Sale sale)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/sales/{sale.Id}", sale);

            response.EnsureSuccessStatusCode();
        }

        public async Task PutSupportAsync(Support support)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/supports/{support.Id}", support);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<SearchResult>> SearchAsync(string query)
        {
            SearchTerm term = new SearchTerm
            {
                Query = query
            };

            var response = await httpClient.PostAsJsonAsync($"/api/search", term);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<List<SearchResult>>();
        }
    }
}
