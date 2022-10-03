using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
            
            => name.Replace("\"","")    
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace(" ", "-")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("¨", "")
            .Replace("~", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(":", "")
            .Replace(".", "-")
            .Replace("Ö", "O")
            .Replace("ö", "o")
            .Replace("Ü", "U")
            .Replace("ü", "u")
            .Replace("ı", "i")
            .Replace("İ", "i")
            .Replace("ğ", "g")
            .Replace("Ğ", "G")
            .Replace("æ", "")
            .Replace("ß", "ss")
            .Replace("â", "a")
            .Replace("î", "i")
            .Replace("ş", "s")
            .Replace("Ş", "S")
            .Replace("Ç", "C")
            .Replace("ç", "c")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "");
        
    }
}
