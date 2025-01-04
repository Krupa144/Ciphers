using Microsoft.AspNetCore.Mvc;

namespace Chpiers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CaesarCipher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EncryptCaesar(string inputText, int key)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Proszê wprowadziæ tekst do zaszyfrowania.";
                return View("CaesarCipher");
            }

            string encryptedText = CaesarEncrypt(inputText, key);
            ViewBag.EncryptedText = encryptedText;
            ViewBag.OriginalText = inputText;
            ViewBag.Key = key;

            return View("CaesarCipher");
        }

        [HttpPost]
        public IActionResult DecryptCaesar(string inputText, int key)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Proszê wprowadziæ tekst do odszyfrowania.";
                return View("CaesarCipher");
            }

            string decryptedText = CaesarEncrypt(inputText, -key);
            ViewBag.DecryptedText = decryptedText;
            ViewBag.OriginalText = inputText;
            ViewBag.Key = key;

            return View("CaesarCipher");
        }

        public IActionResult PolybiusCipher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EncryptPolybius(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Proszê wprowadziæ tekst do zaszyfrowania.";
                return View("PolybiusCipher");
            }

            string encryptedText = PolybiusEncrypt(inputText);
            ViewBag.EncryptedText = encryptedText;
            ViewBag.OriginalText = inputText;

            return View("PolybiusCipher");
        }

        [HttpPost]
        public IActionResult DecryptPolybius(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Proszê wprowadziæ tekst do odszyfrowania.";
                return View("PolybiusCipher");
            }

            string decryptedText = PolybiusDecrypt(inputText);
            ViewBag.DecryptedText = decryptedText;
            ViewBag.OriginalText = inputText;

            return View("PolybiusCipher");
        }

        private string CaesarEncrypt(string input, int key)
        {
            char[] buffer = input.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((letter + key - offset + 26) % 26 + offset);
                }
                buffer[i] = letter;
            }

            return new string(buffer);
        }

        private string PolybiusEncrypt(string input)
        {
            var table = new char[,]
            {
                {'A', 'B', 'C', 'D', 'E'},
                {'F', 'G', 'H', 'I', 'K'}, 
                {'L', 'M', 'N', 'O', 'P'},
                {'Q', 'R', 'S', 'T', 'U'},
                {'V', 'W', 'X', 'Y', 'Z'}
            };

            input = input.ToUpper().Replace("J", "I");
            string result = "";

            foreach (char letter in input)
            {
                if (char.IsLetter(letter))
                {
                    for (int row = 0; row < 5; row++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (table[row, col] == letter)
                            {
                                result += $"{row + 1}{col + 1} ";
                            }
                        }
                    }
                }
            }

            return result.Trim();
        }

        private string PolybiusDecrypt(string input)
        {
            var table = new char[,]
            {
                {'A', 'B', 'C', 'D', 'E'},
                {'F', 'G', 'H', 'I', 'K'},
                {'L', 'M', 'N', 'O', 'P'},
                {'Q', 'R', 'S', 'T', 'U'},
                {'V', 'W', 'X', 'Y', 'Z'}
            };

            string result = "";
            var pairs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var pair in pairs)
            {
                if (pair.Length == 2 &&
                    int.TryParse(pair[0].ToString(), out int row) &&
                    int.TryParse(pair[1].ToString(), out int col))
                {
                    result += table[row - 1, col - 1];
                }
            }

            return result;
        }
    }
}
