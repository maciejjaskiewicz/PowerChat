using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerChat.Common;

namespace PowerChat.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDateTime _dateTime;

        public HomeController(ILogger<HomeController> logger, 
            IDateTime dateTime)
        {
            _logger = logger;
            _dateTime = dateTime;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Logger test");

            return await Task.FromResult(Json(new
            {
                a = "a",
                d = _dateTime.Now
            }));
        }
    }
}
