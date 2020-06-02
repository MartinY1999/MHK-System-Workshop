using MHKBackend.Data.Models;
using MHKDTO.Responses;

namespace MHKBackend.Infrastructure
{
    public static class MappingExtensions
    {
        public static FinanceResponse MapFinanceResponse(this Finance finance) =>
            MapperWrapper.Mapper.Map<FinanceResponse>(finance);

        public static ProjectResponse MapProjectResponse(this Project project) =>
            MapperWrapper.Mapper.Map<ProjectResponse>(project);

        public static SaleResponse MapSaleResponse(this Sale sale) =>
            MapperWrapper.Mapper.Map<SaleResponse>(sale);

        public static SupportResponse MapSupportResponse(this Support support) =>
            MapperWrapper.Mapper.Map<SupportResponse>(support);
    }
}
