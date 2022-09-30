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
    public class InvoiceFileReadRepository : ReadRepository<F.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
