using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class StatisticDetailsController : Controller
    {
        private readonly WebDatabaseContext _context;
        private readonly ILogger<StatisticDetailsController> _logger;

        public StatisticDetailsController(WebDatabaseContext context, ILogger<StatisticDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/Views/StatisticDetails/StatisticDetails.cshtml");
        }
        [HttpGet]
        public JsonResult GetChartData()
        {   
            var netflixSentMail = _context.Attacks
                .Where(a => a.Type == "Netflix")
                .SelectMany(a => a.SentEmails)
                .Count();
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

            var spotifySentMail = _context.Attacks
                .Where(a => a.Type == "Spotify")
                .SelectMany(a => a.SentEmails)
                .Count();
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

            var facebookSentMail = _context.Attacks
                .Where(a => a.Type == "Facebook")
                .SelectMany(a => a.SentEmails)
                .Count();
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


            var data = new
            {   
                NetflixSentMail = netflixSentMail,
                NetflixSuccessCount = netflixSuccessCount,
                NetflixNotSuccessCount = netflixNotSuccessCount,

                SpotifySentMail = spotifySentMail,
                SpotifySuccessCount = spotifySuccessCount,
                SpotifyNotSuccessCount = spotifyNotSuccessCount,

                FacebookSentMail = facebookSentMail,
                FacebookSuccessCount = facebookSuccessCount,
                FacebookNotSuccessCount = facebookNotSuccessCount
            };

            return Json(data);
        }
    }
}
