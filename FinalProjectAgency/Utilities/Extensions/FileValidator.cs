using Microsoft.AspNetCore.Http;

namespace FinalProjectAgency.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool ValidateType(this IFormFile file, string type="image/")
        {
            if(file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool ValidateSize(this IFormFile file, int kb=2)
        {
            if (file.Length<=kb*1024*1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CreateFile(this IFormFile file,params string[] folders)
        {
            string filename = file.GenerateName();
            string path = folders.Aggregate(Path.Combine);
            path= Path.Combine(path, filename);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }
        public static void DeleteFile(this string filename,string root,params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path= Path.Combine(path, filename);
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static string GenerateName(this IFormFile file)
        {
            int index= file.FileName.LastIndexOf(".");
            string filename=Guid.NewGuid().ToString()+file.FileName.Substring(index);
            return filename;
        }
    }
}
