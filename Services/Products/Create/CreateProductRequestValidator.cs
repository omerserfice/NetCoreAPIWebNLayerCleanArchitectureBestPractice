using App.Repositories.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Products.Create
{
	public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
	{
		private readonly IProductRepository _productRepository;

	

		public CreateProductRequestValidator(IProductRepository productRepository)
		{
			_productRepository = productRepository;

			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ürün ismi gereklidir.")
				.Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
				//.MustAsync(MustUniqueProductNameAsync).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
				//.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

			// price validation
			RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalıdır.");

			RuleFor(x => x.Stock)
				.InclusiveBetween(1, 100).WithMessage("Ürün stok adedi 1 ile 100 arasında olmalıdır.");

		}

		//private async Task<bool> MustUniqueProductNameAsync(string name,CancellationToken cancellationToken)
		//{
		//	return await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
		//}

		//1.yol sync validation
		//private bool MustUniqueProductName(string name)
		//{
		//	return !_productRepository.Where(x => x.Name == name).Any();
		//}


	}
}
