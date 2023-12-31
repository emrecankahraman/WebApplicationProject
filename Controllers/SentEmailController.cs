using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplicationProject.Models;


namespace WebApplicationProject.Controllers
{
    public class SentEmailController : Controller
    {
        private readonly WebDatabaseContext _context;
        private readonly ILogger<SentEmailController> _logger;

        public SentEmailController(WebDatabaseContext context, ILogger<SentEmailController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/Views/SentEmail/SentEmail.cshtml");
        }


        /*public IActionResult Create(SentEmail sentEmail)
        {   
            if (ModelState.IsValid)
            {
                    if (sentEmail.VictimId != null)
                    {
                        var victim = _context.Victims.FirstOrDefault(v => v.VictimId == sentEmail.VictimId);
                        var attack = _context.Attacks.FirstOrDefault(a => a.AttackId == sentEmail.AttackId);
                        if (victim != null)
                        {
                            sentEmail.Içerik = $"Sn. {victim.Name} bir ödül kazandınız.";
                        }
                        if (attack != null)
                        {
                            sentEmail.Içerik += $" {attack.Type} için {attack.Url}";
                        }
                    }   
                // Model verilerini kullanarak yapılacak işlemler (örneğin, veritabanına ekleme)
                // Örnek olarak:
                _context.Add(sentEmail);
                _context.SaveChanges();
                
                return RedirectToAction("Index"); // Başka bir sayfaya yönlendirme
            }

            
            return View(sentEmail);
        }
        */

       
        public IActionResult SentEmail(int startId, int endId, SentEmail sentEmail)
        {
            if (ModelState.IsValid)
            {
                var victimsInRange = _context.Victims
                    .Where(v => v.VictimId >= startId && v.VictimId <= endId)
                    .ToList();
                var attack = _context.Attacks.FirstOrDefault(a => a.AttackId == sentEmail.AttackId);

                foreach (var victim in victimsInRange)
                {
                    var newSentEmail = new SentEmail
                    {
                        VictimId = victim.VictimId,
                        //Description = $"Sn. {victim.Name} bir ödül kazandınız. {attack?.Type} den gelen hediyeyi kabul etmek için <a href='{attack?.Url}'>buraya tıklayın</a>",
                        SentDate = sentEmail.SentDate,
                        AttackId = sentEmail.AttackId,
                        Title = sentEmail.Title
                    };

                    var Email = _context.Add(newSentEmail);
                    _context.SaveChanges();
                    var SavedEmail = _context.SentEmails.FirstOrDefault(a => a.EmailId == Email.Entity.EmailId);
                    SavedEmail.Description = $"Sn. {victim.Name} bir ödül kazandınız. {attack?.Type} den gelen hediyeyi kabul etmek için <a href='{attack?.Url}?EmailId={SavedEmail.EmailId}'>buraya tıklayın</a>";//queryString
                    _context.Update(SavedEmail);
                    _context.SaveChanges();
                    return Redirect($"{attack?.Url}?EmailId={SavedEmail.EmailId}");

                }
  

            }

            return View(sentEmail); // Gerekirse geri dönecek bir view
        }

    }
}
