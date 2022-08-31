using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiniECommerceDbContext>
    {
        public MiniECommerceDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MiniECommerceDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new (dbContextOptionsBuilder.Options);
        }
    }
}
