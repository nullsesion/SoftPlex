using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProductsHandler: IRequestHandler<GetProducts, Result<IReadOnlyList<Product>>>
	{
		private readonly IProductRepository _productRepository;

		public GetProductsHandler(IProductRepository productRepository)
			=> _productRepository = productRepository;

		public async Task<Result<IReadOnlyList<Product>>> Handle(GetProducts request, CancellationToken cancellationToken)
		{
			return await _productRepository.GetProductAsync(request.Page, request.PageSize, cancellationToken);
		}
	}
}
