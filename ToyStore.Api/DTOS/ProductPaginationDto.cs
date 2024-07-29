namespace ToyStore.Api.DTOS
{
    public class ProductPaginationDto
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
