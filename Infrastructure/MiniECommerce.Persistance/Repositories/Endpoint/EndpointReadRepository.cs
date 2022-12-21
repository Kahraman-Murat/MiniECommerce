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
    public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
