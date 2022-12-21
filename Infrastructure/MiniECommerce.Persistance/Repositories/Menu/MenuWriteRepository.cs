using MiniECommerce.Application.Repositories;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories
{
    public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
