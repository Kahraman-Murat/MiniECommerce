using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!

                throw ex;
                return false;
            }
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            //wwwroot/resource/product-images  //"resource/product-images"
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path );

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> values = new();
            
            List<bool> results = new();

            //Random rnd = new Random();
            foreach (IFormFile file in files)
            {
                //string fullPath = Path.Combine(uploadPath, $"{rnd.Next()}{Path.GetExtension(file.FileName)}");
                string fileNewName =  await FileRenameAsync(file.FileName);

                //using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                //await file.CopyToAsync(fileStream);
                //await fileStream.FlushAsync();

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                values.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true))) 
                return values;

            return null;

            //todo sonuclarda hata varsa sunucuda dosya yüklemede hata olduguna ait exception firlat!
        }

    }
}
