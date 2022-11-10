using Microsoft.AspNetCore.SignalR;
using MiniECommerce.Application.Abstractions.Hubs;
using MiniECommerce.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
            => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
      
    }
}
