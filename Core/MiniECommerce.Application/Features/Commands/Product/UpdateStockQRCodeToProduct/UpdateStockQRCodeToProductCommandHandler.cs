using MediatR;
using MiniECommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
    public class UpdateStockQRCodeToProductCommandHandler : IRequestHandler<UpdateStockQRCodeToProductCommandRequest, UpdateStockQRCodeToProductCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateStockQRCodeToProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateStockQRCodeToProductCommandResponse> Handle(UpdateStockQRCodeToProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);
            return new();
        }
    }
}
