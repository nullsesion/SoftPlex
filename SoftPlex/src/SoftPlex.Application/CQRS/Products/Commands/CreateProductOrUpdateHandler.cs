using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateProductOrUpdateHandler : IRequestHandler<CreateOrUpdateProduct, Result>
	{
		private readonly IProductRepository _productRepository;

		public CreateProductOrUpdateHandler(IProductRepository productRepository) 
			=> _productRepository = productRepository;

		public async Task<Result> Handle(CreateOrUpdateProduct request, CancellationToken cancellationToken)
		{
			if(request.Id == Guid.Empty)
				request.Id = Guid.NewGuid();

			Result<Product> productTryCreate = Product.Create(
				request.Id
				, request.Name
				, request.Description  
				, request.ProductVersions
				);
			if (productTryCreate.IsFailure)
				return Result.Failure(productTryCreate.Error);
			
			return await _productRepository.InsertOrUpdateProductAsync(productTryCreate.Value, cancellationToken);
		}
	}
}
