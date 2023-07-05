using Myweb.Domain.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MyWeb2023.Models
{
    public class CommonFunction
    {
        public static void UploadImage(IFormFile file, string pathFile, string? fileName = null)
        {
            var rootFolder = Directory.GetCurrentDirectory();
            string pathbanner = @$"{rootFolder}\wwwroot\data\{pathFile}";
            if (!Directory.Exists(pathbanner)) Directory.CreateDirectory(pathbanner);
            string fileUrl = !string.IsNullOrEmpty(fileName) ? fileName : file.FileName;
            var filepath = Path.Combine(pathbanner, fileUrl);
            using (FileStream filestream = File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
        }

        public static string HashPassword(string password)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
    }
}
