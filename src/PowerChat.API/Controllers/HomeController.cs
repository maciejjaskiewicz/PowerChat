using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PowerChat.API.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(Json(new
            {
                a = "a",
                b = "b"
            }));
        }
    }
}
