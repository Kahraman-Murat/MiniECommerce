using MiniECommerce.Application.Repositories;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = MiniECommerce.Domain.Entities;

namespace MiniECommerce.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<F.File>, IFileReadRepository
    {
        public FileReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
