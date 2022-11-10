using Microsoft.AspNetCore.Builder;
using MiniECommerce.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
