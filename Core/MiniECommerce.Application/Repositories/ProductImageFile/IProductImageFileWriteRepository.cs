using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Repositories
{
    public interface IProductImageFileWriteRepository : IWriteRepository<F.ProductImageFile>
    {
    }
}
