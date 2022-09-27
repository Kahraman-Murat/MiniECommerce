using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, IFileService>();
        }
    }
}
