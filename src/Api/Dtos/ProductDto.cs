namespace Api.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? ImagePath { get; set; }
        public int View { get; set; }
        public int CategoryId { get; set; }
    }
}
