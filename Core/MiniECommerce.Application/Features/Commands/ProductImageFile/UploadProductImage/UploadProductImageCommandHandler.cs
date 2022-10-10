using MediatR;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Commands.ProductImageFile.UploadProductImage
{    
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IStorageService _storageService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(
            IProductReadRepository productReadRepository, 
            IStorageService storageService, 
            IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> datas = await _storageService.UploadAsync("photo-images", request.Files);

            E.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            //ister bunu
            //foreach (var r in datas)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {
            //        FileName = d.fileName,
            //        Path = d.pathOrContainerName,
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}

            //ister bunu kullan
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new E.ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<E.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
