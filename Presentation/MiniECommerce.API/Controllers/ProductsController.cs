using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Features.Commands.Product.CreateProduct;
using MiniECommerce.Application.Features.Commands.Product.RemoveProduct;
using MiniECommerce.Application.Features.Commands.Product.UpdateProduct;
using MiniECommerce.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using MiniECommerce.Application.Features.Commands.ProductImageFile.UploadProductImage;
using MiniECommerce.Application.Features.Queries.Product.GetAllProduct;
using MiniECommerce.Application.Features.Queries.Product.GetByIdProduct;
using MiniECommerce.Application.Features.Queries.ProductImageFile.GetProductImages;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Application.ViewModels.Products;
using MiniECommerce.Domain.Entities;
using System.Net;

namespace MiniECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        //private readonly IFileService _fileService;
        private readonly IStorageService _storageService;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IConfiguration _configuration;


        readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository,
            IOrderReadRepository orderReadRepository,
            IWebHostEnvironment webHostEnvironment,
            //IFileService fileService,
            IStorageService storageService,
            IFileReadRepository fileReadRepository,
            IFileWriteRepository fileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IConfiguration configuration,


            IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            _webHostEnvironment = webHostEnvironment;
            //_fileService = fileService;
            _storageService = storageService;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _configuration = configuration;


            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest) //Pagination pagination
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);

            //Task.Delay(1500);
            //var totalCount = _productReadRepository.GetAll(false).Count();
            //var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            //{
            //    p.Id,
            //    p.Name,
            //    p.Stock,
            //    p.Price,
            //    p.CreatedDate,
            //    p.UpdatedDate
            //}).ToList();

            //return Ok( new
            //{
            //    totalCount,
            //    products
            //});

            //okunan dataya update ugulayip auto updateddate kontrolu
            //Order order = await _orderReadRepository.GetByIdAsync("8123086e-6d52-4389-aad7-5eb2a1884124");
            //order.Address = "Eskisehir";
            //await _orderWriteRepository.SaveAsync();

            //customer ve order ekleme aninda auto createddate kontrolü
            //var customerId = Guid.NewGuid();
            //await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Taner" });
            //await _orderWriteRepository.AddAsync(new() { Description = "xcvb", Address = "Esk", CustomerId = customerId });
            //await _orderWriteRepository.SaveAsync();



            //_productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10, CreatedDate = DateTime.UtcNow });
            //_productWriteRepository.SaveAsync();


            //Product p =  await _productReadRepository.GetByIdAsync("087c2754-09a2-4f30-8f52-a28ce13b90a5", false);
            //p.Name = "Tanerrrr";
            //await _productWriteRepository.SaveAsync();





            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){ Id = Guid.NewGuid(), Name="Product 1", Price=100, CreatedDate=DateTime.UtcNow, Stock=10},
            //    new(){ Id = Guid.NewGuid(), Name="Product 2", Price=200, CreatedDate=DateTime.UtcNow, Stock=20},
            //    new(){ Id = Guid.NewGuid(), Name="Product 3", Price=300, CreatedDate=DateTime.UtcNow, Stock=30}
            //});
            //await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest) //string id
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest) //VM_Create_Product model
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            //return Ok(response);
            return StatusCode((int)HttpStatusCode.Created);


            //await _productWriteRepository.AddAsync(new()
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Stock = model.Stock
            //});
            //await _productWriteRepository.SaveAsync();
            //return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)//VM_Update_Product model
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();

            //Product product = await _productReadRepository.GetByIdAsync(model.Id);
            //product.Name = model.Name;
            //product.Stock = model.Stock;
            //product.Price = model.Price;

            //await _productWriteRepository.SaveAsync();
            //return StatusCode((int)HttpStatusCode.Created);
            //return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest) //string id
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();

            //await _productWriteRepository.RemoveAsync(id);
            //await _productWriteRepository.SaveAsync();
            //return Ok(//new
            //{
            //    message = "Silme islemi basarili!"
            //}
            //);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest) //string id
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();


            ////var datas 
            //List<(string fileName, string pathOrContainerName)> datas = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            //Product product = await _productReadRepository.GetByIdAsync(id);

            ////ister bunu
            ////foreach (var r in datas)
            ////{
            ////    product.ProductImageFiles.Add(new()
            ////    {
            ////        FileName = d.fileName,
            ////        Path = d.pathOrContainerName,
            ////        Storage = _storageService.StorageName,
            ////        Products = new List<Product>() { product }
            ////    });
            ////}

            ////ister bunu kullan
            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName,
            //    Products = new List<Product>() { product }

            //}).ToList());

            //await _productImageFileWriteRepository.SaveAsync();
            //return Ok();

            //var datas = await _fileService.UploadAsync("resource\\product-images", Request.Form.Files);
            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path

            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();

            //var datas = await _fileService.UploadAsync("resource\\invoices", Request.Form.Files);
            //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = new Random().Next()
            //}).ToList());
            //await _invoiceFileWriteRepository.SaveAsync();

            //var datas = await _fileService.UploadAsync("resource\\files", Request.Form.Files);
            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new MiniECommerce.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path

            //}).ToList());
            //await _fileWriteRepository.SaveAsync();

            //var d1 = _fileReadRepository.GetAll(false);
            //var d2 = _productImageFileReadRepository.GetAll(false);
            //var d3 = _invoiceFileReadRepository.GetAll(false);

            //Azure kullanirken container adinda slash bulunamaz
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName

            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();


        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest) // string id
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);


            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


            ////await Task.Delay(2000);
            //return Ok(product.ProductImageFiles.Select(p=> new
            //{
            //    Path = $"{_configuration["BaseStorageUrl"]}\\{p.Path}",
            //    p.FileName,
            //    p.Id
            //}));
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId) // string id,
        {
            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();

            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            //ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            //product.ProductImageFiles.Remove(productImageFile);
            //await _productWriteRepository.SaveAsync();
            //return Ok();
        }

        //private readonly IProductService _productService;

        //public ProductsController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    var products = _productService.GetProducts();
        //    return Ok(products);
        //}
    }
}
