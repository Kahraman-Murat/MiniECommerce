using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Application.Services;
using MiniECommerce.Infrastructure.Operations;
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

        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            string newFileName = await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}"; //{extension}

                return await SuitableFileName(path, newFileName, extension) + extension;
            });

            return newFileName;
        }

        private async Task<string> SuitableFileName(string path, string fileName, string extension)
        {
            string dosya = $"{path}\\{fileName}{extension}";
            if (File.Exists(dosya))
            {
                //Dosya adini tirelerden bol liste yap
                string[] fileParts = fileName.Split('-');

                //listede 1 den fazla parca var mi
                if(fileParts.Count() > 1)
                {
                    //son parcayi al
                    string lastItem = fileParts[fileParts.Count() - 1];

                    //son parca sayi ise
                    int a = 0;                    
                    if (int.TryParse(lastItem, out a))
                    {
                        //sayiyi arttir
                        a++;
                        //son parca haric tirelerle birlestir
                        string lastFileName = string.Join("-", fileParts.SkipLast(1));
                        //tire ve son atran sayiyi ekle
                        lastFileName += "-" + a.ToString();
                        return await SuitableFileName(path, lastFileName, extension);
                    }
                    else
                    {
                        return await SuitableFileName(path, $"{fileName}-2", extension);
                    }
                }
                else
                {
                    return await SuitableFileName(path, $"{fileName}-2", extension);
                }                
            }
            else
            {
                return fileName;
            }
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
                string fileNewName =  await FileRenameAsync(uploadPath, file.FileName);

                //using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                //await file.CopyToAsync(fileStream);
                //await fileStream.FlushAsync();

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                values.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true))) 
                return values;

            return null;

            //todo sonuclarda hata varsa sunucuda dosya yüklemede hata olduguna ait exception firlat!
        }

    }
}
