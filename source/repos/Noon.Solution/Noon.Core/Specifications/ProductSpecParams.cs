namespace Noon.Core.Specifications
{
    public class ProductSpecParams
    {
        private const int mazPageSize = 10;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > mazPageSize ? value : value; }
        }

        public int PageIndex { get; set; } = 1;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
    }
}