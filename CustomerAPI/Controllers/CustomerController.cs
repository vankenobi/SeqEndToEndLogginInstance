using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly Serilog.ILogger _logger;

        public CustomerController(HttpClient httpClient, Serilog.ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet]
        [Route("CustomerGet")]
        public async Task<string> CustomerGet()
        {
            string apiUrl = "https://localhost:7123/api/Products/ProductGet"; //url

            HttpRequest request = HttpContext.Request;
            string requestId = request.HttpContext.Items["requestId"]?.ToString() ?? "";
            _logger.Information("CustomerGet method started in CustomerAPI service. Request Id: {requestId}", requestId);

            if (string.IsNullOrEmpty(requestId))
                throw new ArgumentNullException(nameof(requestId));

            _httpClient.DefaultRequestHeaders.Add("requestId", requestId);
          
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            _logger.Information("CustomerGet method finished in CustomerAPI service. Request Id: {requestId}", requestId);

            return responseContent;
        }
    }
}

