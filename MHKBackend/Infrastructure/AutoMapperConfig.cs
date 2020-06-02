using MHKBackend.Data.Models;
using MHKDTO.Responses;

namespace MHKBackend.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            MapperWrapper.Initialize(cfg =>
            {
                cfg.CreateMap<Finance, FinanceResponse>();
                cfg.CreateMap<Project, ProjectResponse>();
                cfg.CreateMap<Sale, SaleResponse>();
                cfg.CreateMap<Support, SupportResponse>();
            });

            MapperWrapper.AssertConfigurationIsValid();
        }
    }
}
