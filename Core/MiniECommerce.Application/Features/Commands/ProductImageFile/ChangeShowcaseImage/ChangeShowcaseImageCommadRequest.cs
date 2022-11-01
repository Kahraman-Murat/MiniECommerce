using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommadRequest : IRequest<ChangeShowcaseImageCommadResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
