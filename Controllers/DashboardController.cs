using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly WebDatabaseContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(WebDatabaseContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Dashboard.cshtml");
        }

    }
}
