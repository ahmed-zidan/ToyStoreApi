namespace ToyStore.Core.SharedModels
{
    public class PaginationDto
    {
        public int? CategoryId{get;set;}
        public string? Search{get;set;}
        public string? Sorting{get;set;}
        public int MinPrice{get;set;}
        public int MaxPrice { get;set;}
        public int PageIdx { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public List<int>? Sizes { get; set; }
        public List<int>? Colors { get; set; }
    }
}
