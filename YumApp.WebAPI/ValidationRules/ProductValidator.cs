using FluentValidation;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.ValidationRules
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.ProductName).NotEmpty().WithMessage("Lütfen Ürün Adını Boş Bırakmayınız !");
            RuleFor(x=>x.ProductName).MinimumLength(2).WithMessage("Lütfen Ürüne En Az 2 Karakter Girişi Yapınız !");
            RuleFor(x=>x.ProductName).MaximumLength(50).WithMessage("Lütfen Ürüne En Fazla 50 Karakter Girişi Yapınız !");


            RuleFor(x => x.ProductPrice).NotEmpty().WithMessage("Lütfen Ürünün Fiyatını Giriniz").GreaterThan(0).WithMessage("Ürün Fiyatı Negatif Olamaz!").LessThan(5000).WithMessage("Ürün Fiyatı 5000'den Büyük Olamaz");

            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün Açıklaması Boş Bırakılamaz !");
        }
    }
}
