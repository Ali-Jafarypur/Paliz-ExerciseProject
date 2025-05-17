namespace BlazorApp1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;

        public string SuplierName { get; set; } = string.Empty;
        public uint ProductCount { get; set; } = 0;
        public uint ProductPrice { get; set; } = 0;
    }
}
