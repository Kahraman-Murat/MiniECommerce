using FluentValidation;
using MiniECommerce.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen ürün adini gíriniz.")
                .MaximumLength(150)
                .MinimumLength(5).WithMessage("Lütfen ürün adini 5-150 karakter arasinda giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen stok miktari gíriniz.")
                .Must(s => s >= 0).WithMessage("Stok bilgisi negatif olamaz !");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen fiyati gíriniz.")
                .Must(s => s >= 0).WithMessage("Fiyat bilgisi negatif olamaz !");
        }
    }
}
