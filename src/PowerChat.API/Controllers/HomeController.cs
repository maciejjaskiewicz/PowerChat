using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerChat.Application.Common.Interfaces;
using PowerChat.Common;
using PowerChat.Domain.Entities;

namespace PowerChat.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDateTime _dateTime;
        private readonly IPowerChatDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, 
            IDateTime dateTime, 
            IPowerChatDbContext dbContext)
        {
            _logger = logger;
            _dateTime = dateTime;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Logger test");

            return await Task.FromResult(Json(new
            {
                a = "a",
                d = _dateTime.Now,
                c = _dbContext.Channels.ToList()
            }));
        }

        public async Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            var testChannel = new Channel
            {
                Name = Guid.NewGuid().ToString()
            };

            _dbContext.Channels.Add(testChannel);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
