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
                ViewBag.Result = "Wprowadü tekst do zaszyfrowania!";
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

        public IActionResult PolybiusCipher() => View();

        [HttpPost]
        public IActionResult PolybiusCipher(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                ViewBag.Result = "Wprowadü tekst do zaszyfrowania!";
            }
            else
            {
                ViewBag.Result = EncryptWithPolybiusCipher(inputText);
            }

            return View();
        }

        private string EncryptWithPolybiusCipher(string text)
        {
            // Tabela szyfru Polibiusza
            var polybiusSquare = new Dictionary<char, string>
    {
        {'A', "11"}, {'B', "12"}, {'C', "13"}, {'D', "14"}, {'E', "15"},
        {'F', "21"}, {'G', "22"}, {'H', "23"}, {'I', "24"}, {'J', "24"}, // I i J majπ ten sam kod
        {'K', "25"}, {'L', "31"}, {'M', "32"}, {'N', "33"}, {'O', "34"},
        {'P', "35"}, {'Q', "41"}, {'R', "42"}, {'S', "43"}, {'T', "44"},
        {'U', "45"}, {'V', "51"}, {'W', "52"}, {'X', "53"}, {'Y', "54"},
        {'Z', "55"}
    };

            // Zamiana tekstu na wielkie litery i szyfrowanie
            text = text.ToUpper();
            var encrypted = text
                .Where(c => char.IsLetter(c)) // Ignorowanie znakÛw nie bÍdπcych literami
                .Select(c => polybiusSquare.ContainsKey(c) ? polybiusSquare[c] : "")
                .ToArray();

            return string.Join(" ", encrypted);
        }

    }
}
