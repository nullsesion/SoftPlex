using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateProductOrUpdateHandler : IRequestHandler<CreateOrUpdateProduct, Result<bool, ErrorList>>
	{
		private readonly IProductRepository _productRepository;

		public CreateProductOrUpdateHandler(IProductRepository productRepository) 
			=> _productRepository = productRepository;

		public async Task<Result<bool, ErrorList>> Handle(CreateOrUpdateProduct request, CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			if (request.Id == Guid.Empty)
				request.Id = Guid.NewGuid();

			Result<Product, ErrorList> productTryCreate = Product.Create(
				request.Id
				, request.Name
				, request.Description  
				, request.ProductVersions
				);

			if (productTryCreate.IsFailure)
			{
				errorList.AddErrors(productTryCreate.Error.Errors);
				return Result.Failure<bool, ErrorList>(errorList);//
			}
				
			
			return await _productRepository.InsertOrUpdateProductAsync(productTryCreate.Value, cancellationToken);
		}
	}
}
