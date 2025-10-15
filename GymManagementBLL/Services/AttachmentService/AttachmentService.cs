using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string? Upload(string FolderName, IFormFile File)
        {
            try
            {
                if (FolderName is null || File is null || File.Length == 0) return null;

                if (File.Length > MaxFileSize) return null;

                var Extension = Path.GetExtension(File.FileName).ToLower();
                if (!AllowedExtensions.Contains(Extension)) return null;

                //  C: \Users\Bassem\Downloads\GymManagementSystemSolution\GymManagementPl\wwwroot\Images\{FolderName}
                var FolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", FolderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                // guid Ex = a6dwdf63423.jpg
                var FileName = Guid.NewGuid().ToString() + Extension;


                // C:\Users\Bassem\Downloads\GymManagementSystemSolution\GymManagementPl\wwwroot\Images\{FolderName}\a6dwdf63423.jpg
                var FilePath = Path.Combine(FolderPath, FileName);

                using var FileStream = new FileStream(FilePath, FileMode.Create);
                File.CopyTo(FileStream);

                return FileName;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"failed to Upload file to folder = {FolderName} : {ex}");
                return null;
            }

        }

        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName)) return false;
                var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", FolderName , FileName);

                if(File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to Delete file with name = {FileName} : {ex}");
                return false;
            }
        }

    }
}
