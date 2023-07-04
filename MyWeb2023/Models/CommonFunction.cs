using Myweb.Domain.Models.Entities;

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
    }
}
