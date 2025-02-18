using Microsoft.AspNetCore.Mvc;
using System.Text;



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
                ViewBag.Message = "Please provide text to encrypt.";
                return View("CaesarCipher");
            }

            string encryptedText = CaesarEncrypt(inputText, key);
            ViewBag.EncryptedText = encryptedText;

            return View("CaesarCipher");
        }

        [HttpPost]
        public IActionResult DecryptCaesar(string inputText, int key)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Please provide text to decrypt.";
                return View("CaesarCipher");
            }

            string decryptedText = CaesarEncrypt(inputText, -key);
            ViewBag.DecryptedText = decryptedText;

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
                ViewBag.Message = "Please provide text to encrypt.";
                return View("PolybiusCipher");
            }

            string encryptedText = PolybiusEncrypt(inputText);
            ViewBag.EncryptedText = encryptedText;

            return View("PolybiusCipher");
        }

        [HttpPost]
        public IActionResult DecryptPolybius(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Message = "Please provide text to decrypt.";
                return View("PolybiusCipher");
            }

            string decryptedText = PolybiusDecrypt(inputText);
            ViewBag.DecryptedText = decryptedText;

            return View("PolybiusCipher");
        }

        public IActionResult PlayfairCipher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EncryptPlayfair(string inputText, string key)
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(key))
            {
                ViewBag.Message = "Please provide both text and a key.";
                return View("PlayfairCipher");
            }

            string encryptedText = PlayfairEncrypt(inputText, key);
            ViewBag.EncryptedText = encryptedText;

            return View("PlayfairCipher");
        }

        [HttpPost]
        public IActionResult DecryptPlayfair(string inputText, string key)
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(key))
            {
                ViewBag.Message = "Please provide both text and a key.";
                return View("PlayfairCipher");
            }

            string decryptedText = PlayfairDecrypt(inputText, key);
            ViewBag.DecryptedText = decryptedText;

            return View("PlayfairCipher");
        }

        public IActionResult VigenereCipher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EncryptVigenere(string inputText, string key)
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(key))
            {
                ViewBag.Message = "Please provide both text and a key.";
                return View("VigenereCipher");
            }

            string encryptedText = VigenereEncrypt(inputText, key);
            ViewBag.EncryptedText = encryptedText;

            return View("VigenereCipher");
        }

        [HttpPost]
        public IActionResult DecryptVigenere(string inputText, string key)
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(key))
            {
                ViewBag.Message = "Please provide both text and a key.";
                return View("VigenereCipher");
            }

            string decryptedText = VigenereDecrypt(inputText, key);
            ViewBag.DecryptedText = decryptedText;

            return View("VigenereCipher");
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
            Dictionary<char, string> polybiusDict = GeneratePolybiusDictionary();
            input = input.ToUpper().Replace("J", "I");
            StringBuilder result = new StringBuilder();

            foreach (char letter in input)
            {
                if (polybiusDict.ContainsKey(letter))
                {
                    result.Append(polybiusDict[letter] + " ");
                }
            }
            return result.ToString().Trim();
        }

        private string PolybiusDecrypt(string input)
        {
            Dictionary<string, char> polybiusReverseDict = GeneratePolybiusReverseDictionary();
            string[] pairs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            StringBuilder result = new StringBuilder();

            foreach (string pair in pairs)
            {
                if (polybiusReverseDict.ContainsKey(pair))
                {
                    result.Append(polybiusReverseDict[pair]);
                }
            }
            return result.ToString();
        }

        private Dictionary<char, string> GeneratePolybiusDictionary()
        {
            char[,] table =
            {
        {'A', '�', 'B', 'C', '�'},
        {'D', 'E', '�', 'F', 'G'},
        {'H', 'I', 'J', 'K', 'L'},
        {'�', 'M', 'N', '�', 'O'},
        {'�', 'P', 'R', 'S', '�'},
        {'T', 'U', 'W', 'Y', 'Z'},
        {'�', '�', 'X', 'V', 'Q'}
    };

            Dictionary<char, string> dict = new Dictionary<char, string>();
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    dict[table[row, col]] = $"{row + 1}{col + 1}";
                }
            }
            return dict;
        }

        private Dictionary<string, char> GeneratePolybiusReverseDictionary()
        {
            char[,] table =
            {
        {'A', '�', 'B', 'C', '�'},
        {'D', 'E', '�', 'F', 'G'},
        {'H', 'I', 'J', 'K', 'L'},
        {'�', 'M', 'N', '�', 'O'},
        {'�', 'P', 'R', 'S', '�'},
        {'T', 'U', 'W', 'Y', 'Z'},
        {'�', '�', 'X', 'V', 'Q'}
    };

            Dictionary<string, char> dict = new Dictionary<string, char>();
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    dict[$"{row + 1}{col + 1}"] = table[row, col];
                }
            }
            return dict;
        }

        private string PlayfairEncrypt(string text, string key)
        {
            char[,] grid = GeneratePlayfairGrid(key);
            return ProcessPlayfair(text, grid, true);
        }

        private string PlayfairDecrypt(string text, string key)
        {
            char[,] grid = GeneratePlayfairGrid(key);
            return ProcessPlayfair(text, grid, false);
        }

        private string ProcessPlayfair(string text, char[,] grid, bool encrypt)
        {
            text = text.ToUpper().Replace("J", "I").Replace(" ", "");
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = (i + 1 < text.Length) ? text[i + 1] : 'X';

                if (a == b)
                {
                    b = 'X';
                    i--;
                }

                (int row1, int col1) = FindPosition(a, grid);
                (int row2, int col2) = FindPosition(b, grid);

                if (row1 == row2)
                {
                    result.Append(grid[row1, (col1 + (encrypt ? 1 : 4)) % 5]);
                    result.Append(grid[row2, (col2 + (encrypt ? 1 : 4)) % 5]);
                }
                else if (col1 == col2)
                {
                    result.Append(grid[(row1 + (encrypt ? 1 : 4)) % 5, col1]);
                    result.Append(grid[(row2 + (encrypt ? 1 : 4)) % 5, col2]);
                }
                else
                {
                    result.Append(grid[row1, col2]);
                    result.Append(grid[row2, col1]);
                }
            }

            return result.ToString();
        }

        private (int, int) FindPosition(char c, char[,] grid)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (grid[row, col] == c)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        private char[,] GeneratePlayfairGrid(string key)
        {
            key = key.ToUpper().Replace("J", "I");
            StringBuilder alphabet = new StringBuilder("ABCDEFGHIKLMNOPQRSTUVWXYZ");
            StringBuilder uniqueKey = new StringBuilder();

            foreach (char c in key)
            {
                if (!uniqueKey.ToString().Contains(c) && alphabet.ToString().Contains(c))
                {
                    uniqueKey.Append(c);
                    alphabet.Replace(c.ToString(), string.Empty);
                }
            }

            uniqueKey.Append(alphabet);
            char[,] grid = new char[5, 5];
            for (int i = 0; i < 25; i++)
            {
                grid[i / 5, i % 5] = uniqueKey[i];
            }

            return grid;
        }

        private string VigenereEncrypt(string input, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char letter in input)
            {
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    result.Append((char)((letter + key[keyIndex % key.Length] - 2 * offset) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(letter);
                }
            }

            return result.ToString();
        }

        private string VigenereDecrypt(string input, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char letter in input)
            {
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    result.Append((char)((letter - key[keyIndex % key.Length] + 26) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(letter);
                }
            }

            return result.ToString();
        }
    }
}