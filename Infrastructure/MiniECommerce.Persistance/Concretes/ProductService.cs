using MiniECommerce.Application.Abstractions;
using MiniECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() { Id=Guid.NewGuid(), Name="Product 1", Price=100, Stock=10 },
                new() { Id=Guid.NewGuid(), Name="Product 2", Price=100, Stock=20 },
                new() { Id=Guid.NewGuid(), Name="Product 3", Price=100, Stock=30 },
                new() { Id=Guid.NewGuid(), Name="Product 4", Price=100, Stock=40 },
                new() { Id=Guid.NewGuid(), Name="Product 5", Price=100, Stock=50 }
            };
    }
}
