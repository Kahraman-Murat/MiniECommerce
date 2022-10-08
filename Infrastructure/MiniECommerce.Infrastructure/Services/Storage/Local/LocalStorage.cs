using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Abstractions.Storage.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
            => File.Delete($"{path}\\{fileName}");        

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{Path.Combine(_webHostEnvironment.WebRootPath, path)}\\{fileName}");

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

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            //wwwroot/resource/product-images  //"resource/product-images"
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> values = new();

            //List<bool> results = new();

            //Random rnd = new Random();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(path, file.Name, HasFile);


                //string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                //bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                //values.Add((fileNewName, $"{path}\\{fileNewName}"));
                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                values.Add((fileNewName, $"{path}\\{fileNewName}"));
                //results.Add(result);
            }

            //if (results.TrueForAll(r => r.Equals(true)))
            //    return values;

            //return null;
            return values;

            //todo sonuclarda hata varsa sunucuda dosya yüklemede hata olduguna ait exception firlat!
        }
    }
}
