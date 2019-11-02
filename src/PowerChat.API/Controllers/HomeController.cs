using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PowerChat.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Logger test");

            return await Task.FromResult(Json(new
            {
                a = "a",
                b = "b"
            }));
        }
    }
}
