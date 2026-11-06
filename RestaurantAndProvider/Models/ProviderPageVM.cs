namespace RestaurantAndProvider.Models
{
    public class ProviderPageVM
    {
        public List<Provider>? Providers { get; set; }
        public string? SearchString { get; set; }
        public string? SortOrder { get; set; }
        public int? PageIndex { get; set; }
        public int? PageCount { get; set; }
    }
}
