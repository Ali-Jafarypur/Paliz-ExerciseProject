using BlazorApp1.Components.Pages;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using static BlazorApp1.Components.Pages.Home;

namespace BlazorApp1.Services
{
    public class FileService : FileServerServiceBase
    {
        private string Apiurl => CreateApiUrl("Product");


        public List<Models.Product>? GetElements()
        {
            return GetJson<List<Models.Product>>($"{Apiurl}/ProductList");
        }
        //public Response<Home.MyElement?> GetElement(int id)
        //{
        //    return GetJson<Response<Home.MyElement>>($"{Apiurl}/Product/{id}");
        //}
    }
}