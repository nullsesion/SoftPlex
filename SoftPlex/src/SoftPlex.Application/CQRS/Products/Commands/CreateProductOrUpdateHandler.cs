using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateProductOrUpdateHandler : IRequestHandler<CreateOrUpdateProduct, Result<bool, ErrorList>>
	{
		private readonly IProductRepository _productRepository;
		private readonly IValidator<CreateOrUpdateProduct> _validator;

		public CreateProductOrUpdateHandler(IProductRepository productRepository, IValidator<CreateOrUpdateProduct> validator)
		{
			_productRepository = productRepository;
			_validator = validator;
		}

		public async Task<Result<bool, ErrorList>> Handle(CreateOrUpdateProduct request, CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			ValidationResult? validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (validationResult is null)
			{
				errorList.AddError(new Error("Server Error", ErrorType.ServerError, null, request.Product.Id));
				return Result.Failure<bool, ErrorList>(errorList);
			}
			if (!validationResult.IsValid)
			{
				foreach (ValidationFailure? err in validationResult.Errors)
				{
					if (err is null)
						continue;

					errorList.AddError(Error.Deserialize(err.ErrorMessage));
				}

				return errorList;
			}

			return Result.Success<bool, ErrorList>(true);
			/*
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
			*/
		}
	}
}
