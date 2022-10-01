using FluentValidation.AspNetCore;
using MiniECommerce.Application.Validators.Products;
using MiniECommerce.Infrastructure;
using MiniECommerce.Infrastructure.Enums;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using MiniECommerce.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfraStructureServices();


builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(StorageType.Azure);

// Gelen belirli isteklere acik olma politikasi icin
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyMethod().AllowAnyHeader()
));

// Tüm gelen isteklere acik olma politikasi icin
// builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
// policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
// ));

builder.Services.AddControllers(options=> options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

//UseCors UseRouting ile UseAuthentication arasinda kullanilir
app.UseCors(); //"myclients"
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
