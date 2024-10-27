using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SoftPlex.Application.DtoModels;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;
using SoftPlex.Domain.ValueObject;

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

			List<Domain.ProductVersion> listPv = new List<Domain.ProductVersion>();
			foreach (ProductVersionDto item in request.Product.ProductVersions)
			{
				Result<SizeBox, ErrorList> trysizeBox = SizeBox.Create(item.Width, item.Height, item.Length);
				if (trysizeBox.IsFailure)
					continue;
				Result<Domain.ProductVersion, ErrorList> res = Domain.ProductVersion.Create(
					item.Id
					, item.ProductId
					, item.Name
					, item.Description
					, trysizeBox.Value
					, DateTime.Now);
				if(res.IsSuccess)
					listPv.Add(res.Value);
			}
			Result<Product, ErrorList> productTryCreate = Product.Create(
				(request.Product.Id == Guid.Empty? Guid.NewGuid(): request.Product.Id)
				, request.Product.Name
				, request.Product.Description
				, listPv
			);

			if (productTryCreate.IsFailure)
			{
				errorList.AddErrors(productTryCreate.Error.Errors);
				return Result.Failure<bool, ErrorList>(errorList);//
			}


			Result<bool, ErrorList> resultDb = await _productRepository.InsertOrUpdateProductAsync(productTryCreate.Value, cancellationToken);

			if (resultDb.IsSuccess)
			{
				return Result.Success<bool, ErrorList>(true);
			}
			else
			{
				return Result.Failure<bool, ErrorList>(resultDb.Error);
			}
		}
	}
}
