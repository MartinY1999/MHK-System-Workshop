using MHKDTO.Responses;

namespace MHKDTO
{
    public class SearchResult
    {
        public SearchResultType Type { get; set; }
        public FinanceResponse Finance { get; set; }
        public ProjectResponse Project { get; set; }
        public SaleResponse Sale { get; set; }
        public SupportResponse Support { get; set; }
    }

    public enum SearchResultType
    {
        Finance,
        Project,
        Sale,
        Support
    }
}
