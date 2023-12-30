
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;


namespace WebApplicationProject.Controllers
{
    public class SpotifyLoginController : Controller
    {
        private readonly WebDatabaseContext _context;
        private readonly ILogger<SpotifyLoginController> _logger;

        public SpotifyLoginController(WebDatabaseContext context, ILogger<SpotifyLoginController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("~/Views/SpotifyLogin/SpotifyLogin.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLogin(string Email, string Password)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Veritabanına ekleme işlemi
                var SpotifyLogin = new SpotifyLogin
                {
                    Email = Email, // Örnek olarak fname'i Email alanına atadım
                    Password = Password // Örnek olarak lname'i Password alanına atadım
                };

                _context.Add(SpotifyLogin);
                await _context.SaveChangesAsync();
                return View("Index.cshtml");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
        }

    }
}

