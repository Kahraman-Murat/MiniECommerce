using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Application;
using MiniECommerce.Application.Validators.Products;
using MiniECommerce.Infrastructure;
using MiniECommerce.Infrastructure.Enums;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Infrastructure.Services.Storage.Azure;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using MiniECommerce.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfraStructureServices();
builder.Services.AddApplicationServices(); 



builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //token degerini kimler / hangi originler / siteler kullanacak
            ValidateIssuer = true, //token degerini kim dagitiyor
            ValidateLifetime = true, //token degeri süresini belirler
            ValidateIssuerSigningKey = true, //token degerinin uygulamamiza ait security key verisi

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
