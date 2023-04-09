using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly Serilog.ILogger _logger;

        public ProductsController(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ProductGet")]
        public IActionResult ProductGet()
        {
            HttpRequest request = HttpContext.Request;
            string requestId = request.HttpContext.Items["requestId"].ToString();

            _logger.Information("ProductGet method started in ProductAPI service. RequestId: {requestId}", requestId);
            _logger.Information("ProductGet method finished in ProductAPI service. RequestId: {requestId}", requestId);

            return Ok();
        }
    }
}

