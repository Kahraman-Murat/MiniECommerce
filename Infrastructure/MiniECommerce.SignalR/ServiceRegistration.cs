using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Hubs;
using MiniECommerce.SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddSignalR();
        }
    }
}
