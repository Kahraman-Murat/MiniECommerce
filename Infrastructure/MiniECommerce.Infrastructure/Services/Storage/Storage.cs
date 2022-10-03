using MiniECommerce.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
        {
            string newFileName = await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}"; //{extension}

                return await SuitableFileName(pathOrContainerName, newFileName, extension, hasFileMethod ) + extension;
            });

            return newFileName;
        }

        private async Task<string> SuitableFileName(string pathOrContainerName, string fileName, string extension, HasFile hasFileMethod)
        {
            //string dosya = $"{pathOrContainerName}\\{fileName}{extension}";
            //bool d = File.Exists(dosya);

            //if (d)
            if (hasFileMethod(pathOrContainerName,fileName))
            
            {
                //Dosya adini tirelerden bol liste yap
                string[] fileParts = fileName.Split('-');

                //listede 1 den fazla parca var mi
                if (fileParts.Count() > 1)
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
                        return await SuitableFileName(pathOrContainerName, lastFileName, extension, hasFileMethod);
                    }
                    else
                    {
                        return await SuitableFileName(pathOrContainerName, $"{fileName}-2", extension, hasFileMethod);
                    }
                }
                else
                {
                    return await SuitableFileName(pathOrContainerName, $"{fileName}-2", extension, hasFileMethod);
                }
            }
            else
            {
                return fileName;
            }
        }

    }
}
