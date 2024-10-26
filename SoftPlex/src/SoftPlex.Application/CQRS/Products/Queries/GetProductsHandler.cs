using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProductsHandler: IRequestHandler<GetProducts, Result<IReadOnlyList<Product>, ErrorList>>
	{
		private readonly IProductRepository _productRepository;
		private readonly IValidator<GetProducts> _validator;

		public GetProductsHandler(IProductRepository productRepository, IValidator<GetProducts> validator)
		{
			_productRepository = productRepository;
			_validator = validator;
		}

		public async Task<Result<IReadOnlyList<Product>, ErrorList>> Handle(GetProducts request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				ErrorList errorList = new ErrorList();
				foreach (ValidationFailure? err in validationResult.Errors)
				{
					if(err is null)
						continue;
					errorList.AddError(new Error(err.ErrorMessage,ErrorType.Validation,err.PropertyName,null));
				}
				
				return errorList;
			}
				

			return await _productRepository.GetProductAsync(request.Page, request.PageSize, cancellationToken);
		}
	}
}
