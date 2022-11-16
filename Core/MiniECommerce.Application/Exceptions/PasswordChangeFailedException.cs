using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("Sifre güncellenirken bir sorun olustu.")
        {
        }

        public PasswordChangeFailedException(string? message) : base(message)
        {
        }

        public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
