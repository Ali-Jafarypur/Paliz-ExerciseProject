
using BlazorApp1.Components.Pages;
using BlazorApp1.Models;
using BlazorApp1.Repository;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Net.Http.Json;
using static BlazorApp1.Components.Pages.Home;

namespace BlazorApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productRepository.AddProduct(product);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
            //try
            //{
            //    var products = _productRepository.GetAllProducts();
            //    return Ok(products);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "An error occurred while getting all products.");
            //    return StatusCode(500, "Internal server error");
            //}
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id; // int.Parse(id); Ensure the ID is set
            _productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.DeleteProduct(product);
                return Ok();
            }
            return NotFound();
        }

        // -----------------

        private static readonly List<Models.Product> objects = new List<Models.Product>
            {
                new() {ProductName="p1", ClassName="C1", BrandName="B1", SuplierName="S1", ProductCount=1, ProductPrice=10000},
                new() {ProductName="p2", ClassName="C2", BrandName="B2", SuplierName="S2", ProductCount=2, ProductPrice=20000},
                new() {ProductName="p3", ClassName="C2", BrandName="B3", SuplierName="S3", ProductCount=3, ProductPrice=30000},
                new() {ProductName="p4", ClassName="C3", BrandName="B4", SuplierName="S4", ProductCount=4, ProductPrice=40000},
                new() {ProductName="p5", ClassName="C3", BrandName="B5", SuplierName="S5", ProductCount=5, ProductPrice=50000},
                new() {ProductName="p6", ClassName="C3", BrandName="B6", SuplierName="S6", ProductCount=6, ProductPrice=60000},
                new() {ProductName="p7", ClassName="C4", BrandName="B7", SuplierName="S7", ProductCount=7, ProductPrice=70000},
                new() {ProductName="p8", ClassName="C4", BrandName="B8", SuplierName="S8", ProductCount=8, ProductPrice=80000},
                new() {ProductName="p9", ClassName="C4", BrandName="B9", SuplierName="S9", ProductCount=9, ProductPrice=90000},
                new() {ProductName="p10", ClassName="C4", BrandName="B10", SuplierName="S10", ProductCount=10, ProductPrice=100000},
            };

        [HttpGet("ProductList")]
        public ActionResult<List<Models.Product>> GetProducts()
        {
            return Ok(objects);
        }

        //[HttpGet("Product/{id}")]
        //public Response<Home.MyElement> GetProduct(int id)
        //{
        //    try
        //    {
        //        var x = objects.SingleOrDefault(x => x.Id == id);
        //        return new Response<MyElement>()
        //        {
        //            Data = x,
        //            status = x == null ? 404 : 200,
        //            message = x == null ? "not found" : "ok"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<MyElement>()
        //        {
        //            Data = null,
        //            status = 500,
        //            message = ex.Message
        //        };
        //    }
        //}

        //private readonly HttpClient _httpClient;

        //public ProductController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        //[HttpGet("external")]
        //public async Task<ActionResult<List<Home.MyElement>>> GetExternalProducts()
        //{
        //    try
        //    {
        //        // Replace with your actual API endpoint
        //        var response = await _httpClient.GetFromJsonAsync<List<Home.MyElement>>("https://api.example.com/products");
        //        return Ok(response);
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        return StatusCode(500, $"Internal server error: {e.Message}");
        //    }
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
