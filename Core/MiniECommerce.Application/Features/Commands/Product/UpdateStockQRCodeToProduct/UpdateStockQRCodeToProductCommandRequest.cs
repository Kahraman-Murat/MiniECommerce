using MediatR;

namespace MiniECommerce.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
    public class UpdateStockQRCodeToProductCommandRequest : IRequest<UpdateStockQRCodeToProductCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}