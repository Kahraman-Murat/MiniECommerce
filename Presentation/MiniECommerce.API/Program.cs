using FluentValidation.AspNetCore;
using MiniECommerce.Application.Validators.Products;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
// Gelen belirli isteklere acik olma politikasi icin
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyMethod().AllowAnyHeader()
));

// T�m gelen isteklere acik olma politikasi icin
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
