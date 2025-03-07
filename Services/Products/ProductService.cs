using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;


namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork,IValidator<CreateProductRequest> createProductRequestValidator,IMapper mapper) : IProductService
    {
		public async Task<ServiceResult<List<ProductDto>>> GetAllAsyncList()
		{
			var products = await productRepository.GetAll().ToListAsync();

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

			return ServiceResult<List<ProductDto>>.Success(productsAsDto);
		}
		public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = mapper.Map<List<ProductDto>>(products);


			return new ServiceResult<List<ProductDto>>()
            {
                Data = productsAsDto
            };

        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
               return  ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);
            }

			var productsAsDto = mapper.Map<ProductDto>(product);

			return ServiceResult<ProductDto>.Success(productsAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id)
                ,$"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        { 
            // fast fail -> önce olumsuz duurmları kontrol et.

            // guard clauses 

			var product = await productRepository.GetByIdAsync(id);
			if (product is null)
			{
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
			}

			var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();

			if (isProductNameExist)
			{
				return ServiceResult.Fail( "güncellenecek ürün bulunamadı.", HttpStatusCode.BadRequest);
			}

			//product.Name = request.Name;
   //         product.Price = request.Price;
   //         product.Stock = request.Stock;


            product = mapper.Map(request, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
		}

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

		public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			
			var productsAsDto = mapper.Map<List<ProductDto>>(products);

			return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

		public async Task<ServiceResult> DeleteAsync(int id)
		{
			var product = await productRepository.GetByIdAsync(id);
			if (product is null)
			{
				return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
			}

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
		}

		
	}
}
