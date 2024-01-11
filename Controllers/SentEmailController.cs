using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplicationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

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
        public IActionResult smptEmailSender()
        {
            try
            {
                SendEmailToVictims();
                _logger.LogInformation("Tüm Victim kayıtlarına e-posta başarıyla gönderildi.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("E-posta gönderilirken bir hata oluştu: " + ex.Message);
                return StatusCode(500);
            }
        }

        private void SendEmailToVictims()
        {
            string fromMail = "stmpstmp8@gmail.com";
            string fromPassword = "dkrq ofth kgxj vzqw";

            var victims = _context.Victims.ToList();

            foreach (var victim in victims)
            {
                string toAddress = victim.Email;

                var sentEmail = _context.SentEmails.FirstOrDefault();
                string subject = sentEmail?.Title ?? "Varsayılan Konu";
                string description = sentEmail?.Description ?? "Varsayılan Açıklama";

                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(toAddress));
                message.Body = "<html><body>" + description + "</body></html>";
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.IsBodyHtml = true;

                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(message);

            }
        }


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
                    SavedEmail.Description = $"Sn. {victim.Name} bir ödül kazandınız. {attack?.Type} den gelen hediyeyi kabul etmek için" +
                        $" <a href='{attack?.Url}?EmailId={SavedEmail.EmailId}'>buraya tıklayın</a>";//queryString
                    _context.Update(SavedEmail);
                    _context.SaveChanges();
                    //return Redirect($"{attack?.Url}?EmailId={SavedEmail.EmailId}");

                }
                return RedirectToAction("Index");


            }

            return View(sentEmail); // Gerekirse geri dönecek bir view
        }
        public IActionResult ProcessEmailFile()
        {
            string filePath = @"C:\Users\Tarık\Documents\Emails.txt"; // Dosya yolunu belirtin

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                try
                {
                    string line;
                    var victims = new List<Victim>(); // Eklenecek Victim nesnelerini tutmak için bir liste oluşturuldu

                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        if (values.Length == 4) // 4 sütun olduğunu varsayalım: VictimId, Name, Surname, Email
                        {
                            int existingVictimId;
                            if (int.TryParse(values[0], out existingVictimId))
                            {
                                var victim = new Victim
                                {
                                    // existingVictimId'yi doğrudan VictimId sütununa aktarma
                                    Name = values[1],
                                    Surname = values[2],
                                    Email = values[3]
                                };

                                victims.Add(victim); // Veritabanına eklemek üzere Victim nesnesini listeye ekle
                            }
                        }
                    }

                    _context.Victims.AddRange(victims); // Tüm Victim nesnelerini veritabanına ekle
                    _context.SaveChanges(); // Değişiklikleri kaydet

                    return RedirectToAction("Index"); // veya istediğiniz bir yönlendirmeyi gerçekleştirin
                }
                catch (Exception ex)
                {
                    // Hata durumunda gerekli işlemleri yapabilirsiniz
                    Console.WriteLine("Hata: " + ex.Message);
                    return View("Error"); // Hata görünümünü göstermek için bir View döndürebilirsiniz
                }
                finally
                {
                    reader.Close(); // StreamReader'ı kapatın
                }
            }
        }
        public IActionResult GetSentEmailStatistics()
        {
            var sentEmailData = _context.SentEmails
                 .GroupBy(email => email.SentDate)
                 .Select(group => new
                 {
                     Date = group.Key,
                     EmailCount = group.Count()
                 })
            .OrderBy(item => item.Date)
            .ToList();


            var labels = sentEmailData.Select(item => Convert.ToDateTime(item.Date).ToString("dd-MM-yyyy")).ToArray();
            var data = sentEmailData.Select(item => item.EmailCount).ToArray();

            return Json(new { labels, data });
        }

        [HttpGet]
        public JsonResult GetChartData()
        {
            
            var netflixSuccessCount = _context.Attacks
        .Where(a => a.Type == "Netflix")
        .SelectMany(a => a.SentEmails)
        .SelectMany(se => se.ClickedMails)
            .Count(c => c.Success == true);
           
            
            var netflixNotSuccessCount = _context.Attacks
       .Where(a => a.Type == "Netflix")
       .SelectMany(a => a.SentEmails)
       .SelectMany(se => se.ClickedMails) 
           .Count(c => c.Success == false);


            var facebookSuccessCount = _context.Attacks
                .Where(a => a.Type == "Facebook")
                .SelectMany(a => a.SentEmails)
                .SelectMany(se => se.ClickedMails)
                .Count(c => c.Success == true);


            var facebookNotSuccessCount = _context.Attacks
                .Where(a => a.Type == "Facebook")
                .SelectMany(a => a.SentEmails)
                .SelectMany(se => se.ClickedMails)
                .Count(c => c.Success == false);

            var spotifySuccessCount = _context.Attacks
                .Where(a => a.Type == "Spotify")
                .SelectMany(a => a.SentEmails)
                .SelectMany(se => se.ClickedMails)
                .Count(c => c.Success == true);
            var spotifyNotSuccessCount = _context.Attacks
                .Where(a => a.Type == "Spotify")
                .SelectMany(a => a.SentEmails)
                .SelectMany(se => se.ClickedMails)
                .Count(c => c.Success == false);

            var clickedMailSuccessCount = _context.ClickedMails
                .Where(c => c.Success)
                .Select(c => c.EmailId)
                .Distinct()
                .Count();

            var sentEmailCount = _context.SentEmails.Count();
            var clickedEmailCount = _context.ClickedMails.Count();

            var data = new
            {
                ClickedEmailCount = clickedEmailCount,
                ClickedMailSuccessCount = clickedMailSuccessCount,
                SentEmailCount = sentEmailCount,
                NetflixNotSuccessCount = netflixNotSuccessCount,
                FacebookNotSuccessCount = facebookNotSuccessCount,
                SpotifyNotSuccessCount = spotifyNotSuccessCount,

                NetflixSuccessCount = netflixSuccessCount,
                FacebookSuccessCount = facebookSuccessCount,
                SpotifySuccessCount = spotifySuccessCount
            };

            return Json(data);
        }
    }
}


 