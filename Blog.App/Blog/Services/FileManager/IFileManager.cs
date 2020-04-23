using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Services.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
        FileStream StreamImage(string image);
    }
}
