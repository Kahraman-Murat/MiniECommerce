using MediatR;
using MiniECommerce.Application.Abstractions.Hubs;
using MiniECommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(
            IOrderService orderService, 
            IBasketService basketService, 
            IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrderAsync(new()
            {
                Address = request.Address,
                Description = request.Description,
                BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
            });

            await _orderHubService.OrderAddedMessageAsync("Yeni bir siaris geldi.");

            return new();
        }
    }
}
