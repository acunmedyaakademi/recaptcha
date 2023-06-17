using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recapca1.Models;
using System.Diagnostics;
using System.Net;

namespace Recapca1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Post()
        {
            var response = Request.Form["g-recaptcha-response"];
            const string secret = "6LekIKMmAAAAABm9QO6k2azGnORRobRZlEcnCMCL";
            //Kendi Secret keyinizle değiştirin.

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
                TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
            else
                TempData["Message"] = "Güvenlik başarıyla doğrulanmıştır.";
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}