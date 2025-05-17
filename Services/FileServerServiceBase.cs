using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using static System.Net.WebRequestMethods;

namespace BlazorApp1.Services
{
    public class FileServerServiceBase
    {
        private static readonly HttpClient Client;
        //private string _token;
        //private static string ServiceUrl => "http://localhost:8090";
        //private static string ApiKey => "91FF625F-5227-48CD-9B09-DAB81BA25FAC";

        static FileServerServiceBase()
        {
            Client = new HttpClient();
            //if(!string.IsNullOrEmpty(token))
            //    this._token = token;
            //else
            //{
            //    var httpContext = HttpContext.Current;
            //    if (httpContext != null && httpContext.Items["JwtToken"]!=null)
            //    {
            //        _token = httpContext.Items["JwtToken"].ToString();
            //    }
        }

        private HttpClient GetHttpClient()
        {
            //var httpContext = HttpContext.Current;
            //if (httpContext == null)
            //   var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };
            //else
            //{
            //httpClient = httpContext.Items["ContextHttpClient"] as HttpClient;
            //    if (httpClient == null)
            //    {
            //        httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };
            //httpContext.Items["ContextHttpClient"] = httpClient;
            //    }
            //}

            /*
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            if (!httpClient.DefaultRequestHeaders.Contains(Constants.ApiKeyHeaderName))
            {
                httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeaderName, PortalSettings.ApiKey);
            }

            if (!httpClient.DefaultRequestHeaders.Contains(Constants.ClientIpHeaderName))
            {
                httpClient.DefaultRequestHeaders.Add(Constants.ClientIpHeaderName, IntranetUI.GetUserIp());
            }

            if (!string.IsNullOrEmpty(_token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _token);
                */
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            return Client;
        }

        private HttpRequestMessage GetRequestMessage(string uri, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, uri) { Content = content };
            //request.Headers.Add(Constants.ApiKeyHeaderName, WebVariables.ApiKey);
            //request.Headers.Add(Constants.ClientIpHeaderName, IntranetUI.GetUserIp());
            //var httpContext = HttpContext.Current;
            //if (!string.IsNullOrEmpty(httpContext?.User?.Identity?.Name))
            //    request.Headers.Add(Constants.UserIdHeaderName, httpContext.User.Identity.Name);
            //if (httpContext?.Items["UserRoles"] != null)
            //{
            //    request.Headers.Add(Constants.UserRolesHeaderName, httpContext.Items["UserRoles"].ToString());
            //}

            //if (httpContext?.Items["PortalUserName"] != null)
            //{
            //    request.Headers.Add(Constants.UserNameHeaderName, httpContext.Items["PortalUserName"].ToString());
            //}

            return request;
        }

        public string CreateApiUrl(string serviceName)
        {
            return $"http://localhost:5115/api/{serviceName}";
        }

        protected bool Get(string uri)
        {
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Get)).GetAwaiter().GetResult();
            return CheckResponse(response);
            //var response = await GetHttpClient().GetAsync(uri);
            //return CheckResponse(response);
        }

        protected string GetString(string uri)
        {
            try
            {
                var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Get)).GetAwaiter()
                    .GetResult();
                return response.Content.ReadAsStringAsync().Result;
                //return await GetHttpClient().GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }

        protected byte[]? GetByteArray(string uri)
        {
            try
            {
                var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Get)).GetAwaiter()
                    .GetResult();
                return response.StatusCode == HttpStatusCode.OK ? response.Content.ReadAsByteArrayAsync().Result : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        protected T? GetJson<T>(string uri)
        {
            //var response = GetHttpClient().GetAsync(uri).GetAwaiter().GetResult();
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Get)).GetAwaiter().GetResult();
            if (CheckResponse(response) && ValidateJsonContent(response.Content))
            {
                return response.Content.ReadFromJsonAsync<T>().Result;
            }

            return default;
        }
        /*
        protected T GetJson<T>(string uri)
        {
            //var response = GetHttpClient().GetAsync(uri).Result;
            var response = GetHttpClient().SendAsync(getRequestMessage(uri, HttpMethod.Get));
            if (CheckResponse(response) && ValidateJsonContent(response.Content))
            {
                return response.Content.ReadFromJsonAsync<T>().Result;
            }

            return default;
        }
        */

        protected bool Put(string uri)
        {
            //var response = await GetHttpClient().PutAsync(uri, null);
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Put)).GetAwaiter().GetResult();
            return CheckResponse(response);
        }

        protected T? PutJson<T>(string uri, T value)
        {
            return PutJson<T, T>(uri, value);
        }

        protected TResult? PutJson<TValue, TResult>(string uri, TValue value)
        {
            //var response = await GetHttpClient().PutAsJsonAsync(uri, value);
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Put, JsonContent.Create(value)))
                .GetAwaiter().GetResult();
            if (CheckResponse(response) && ValidateJsonContent(response.Content))
            {
                var result = response.Content.ReadFromJsonAsync<TResult>().Result;
                return result;
            }

            return default;
        }

        protected bool Post(string uri)
        {
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Post)).GetAwaiter().GetResult();
            //var response = await GetHttpClient().PostAsync(uri, null);
            return CheckResponse(response);
        }

        protected T? PostJson<T>(string uri, T value)
        {
            return PostJson<T, T>(uri, value);
        }

        protected TResult? PostJson<TValue, TResult>(string uri, TValue value)
        {
            //var response = await GetHttpClient().PostAsJsonAsync(uri, value);
            HttpRequestMessage request;
            if (value is MultipartFormDataContent data)
            {
                request = GetRequestMessage(uri, HttpMethod.Post, data);
            }
            else
            {
                request = GetRequestMessage(uri, HttpMethod.Post, JsonContent.Create(value));
            }



            var response = GetHttpClient().SendAsync(request)
                .GetAwaiter().GetResult();
            if (CheckResponse(response) && ValidateJsonContent(response.Content))
            {
                var result = response.Content.ReadFromJsonAsync<TResult>().Result;
                return result;
            }



            return default;
        }

        protected bool Delete(string uri)
        {
            //var response = await GetHttpClient().DeleteAsync(uri);
            var response = GetHttpClient().SendAsync(GetRequestMessage(uri, HttpMethod.Delete)).GetAwaiter()
                .GetResult();
            return CheckResponse(response);
            //return response.StatusCode;
        }

        private bool CheckResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        private static bool ValidateJsonContent(HttpContent content)
        {
            var mediaType = content?.Headers.ContentType?.MediaType;
            return mediaType != null && mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

    }
}