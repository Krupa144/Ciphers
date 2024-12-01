using Chpiers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chpiers.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
        public IActionResult CaesarCipher() => View();

        [HttpPost]
        public IActionResult CaesarCipher(string inputText, int shift)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                ViewBag.Result = "WprowadŸ tekst do zaszyfrowania!";
            }
            else
            {
                ViewBag.Result = EncryptWithCaesarCipher(inputText, shift);
            }

            return View();
        }

        private string EncryptWithCaesarCipher(string text, int shift)
        {
            shift = shift % 26; 
            return new string(text.Select(c =>
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    return (char)(((c + shift - offset) % 26 + 26) % 26 + offset);
                }
                return c;
            }).ToArray());
        }


    }
}
