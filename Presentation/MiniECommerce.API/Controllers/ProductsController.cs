using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Application.RequestParameters;
using MiniECommerce.Application.Services;
using MiniECommerce.Application.ViewModels.Products;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Repositories;
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
            IConfiguration configuration)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            Task.Delay(1500);
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok( new
            {
                totalCount,
                products
            });

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();
            //return StatusCode((int)HttpStatusCode.Created);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok(//new
            //{
            //    message = "Silme islemi basarili!"
            //}
            );
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
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

            //var datas 
            List<(string fileName, string pathOrContainerName)> datas = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

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

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }

            }).ToList());


            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


            //await Task.Delay(2000);
            return Ok(product.ProductImageFiles.Select(p=> new
            {
                Path = $"{_configuration["BaseStorageUrl"]}\\{p.Path}",
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();
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
