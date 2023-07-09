using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Security.Cryptography;
using System.Text;

namespace MyWeb2023.Models
{
    public class CommonFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">File truyền vào</param>
        /// <param name="pathFile">Đường dẫn</param>
        /// <param name="fileName"></param>
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

        //public List<ObjectMail> GetObjectMails()
        //{
        //    var res = new List<ObjectMail>();
        //    res.Add(new ObjectMail() { Key = "{FirstName}", Value = "Kumo" });
        //    res.Add(new ObjectMail() { Key = "{DateTime.Now}", Value = DateTime.Now.ToString() });
        //    return res;
        //}

        public static async Task SendMail(string key, string mailTo)
        {
            //var objMails = GetObjectMails();
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from_email = new EmailAddress("nguyenvanhoang73qb@gmail.com", "Kumo Shop");
            var subject = "Sending with Kumo Shop";
            var to_email = new EmailAddress("maxgamingtvchannel@gmail.com", "Example User");
            var plainTextContent = "";
            var rootFolder = Directory.GetCurrentDirectory();
            string path = @$"{rootFolder}\wwwroot\template\{key}";
            var htmlContent = System.IO.File.ReadAllText(path);
            //foreach (var item in objMails)
            //{
            //    htmlContent = htmlContent.Replace(item.Key, item.Value);
            //}
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
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
