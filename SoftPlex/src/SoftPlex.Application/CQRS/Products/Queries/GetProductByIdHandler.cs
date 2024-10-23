using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProductByIdHandler : IRequestHandler<GetProductById, Result<Product>>
	{
		private readonly IProductRepository _productRepository;

		public GetProductByIdHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result<Product>> Handle(GetProductById request, CancellationToken cancellationToken)
		{
			return await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
		}
	}
}
