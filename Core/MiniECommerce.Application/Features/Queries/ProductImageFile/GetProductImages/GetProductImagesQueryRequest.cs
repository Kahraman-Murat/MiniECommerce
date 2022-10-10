using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryRequest: IRequest<List<GetProductImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
