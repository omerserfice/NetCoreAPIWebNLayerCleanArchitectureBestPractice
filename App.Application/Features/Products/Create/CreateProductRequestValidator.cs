using App.Application.Contracts.Persistence;
using FluentValidation;


namespace App.Application.Features.Products.Create
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
				

			// price validation
			RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalıdır.");
			RuleFor(x => x.CategoryId)
			.GreaterThan(0).WithMessage("Ürün kategori değeri 0 dan büyük olmalıdır.");
			RuleFor(x => x.Stock)
				.InclusiveBetween(1, 100).WithMessage("Ürün stok adedi 1 ile 100 arasında olmalıdır.");

		}

	}
}
