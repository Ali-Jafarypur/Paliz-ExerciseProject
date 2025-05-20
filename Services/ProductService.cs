using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public class ProductService
    {
        //public ProductService() { }

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            //httpClient.BaseAddress = new Uri("http://localhost:5115/");
            _httpClient = httpClient;
        }

        public async Task AddProduct(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/product", product);
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/product/{id}");
        }

        public async Task<List<Product>?> GetAllProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("http://localhost:5115/api/product");
            //return await _httpClient.GetFromJsonAsync<List<Product>>("api/product");
        }

        public async Task UpdateProduct(int id, Product product)
        {
            await _httpClient.PutAsJsonAsync($"api/product/{id}", product);
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            return await _httpClient.DeleteAsync($"api/product/{id}");
        }
    }
}
