using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain.Shared;


namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class RemoveProductByIdHandler : IRequestHandler<RemoveProductById, Result<bool, ErrorList>>
	{
		private readonly IProductRepository _productRepository;

		public RemoveProductByIdHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result<bool, ErrorList>> Handle(RemoveProductById request, CancellationToken cancellationToken)
		{
			return await _productRepository.RemoveProductByIdAsync(request.Id, cancellationToken);
		}
	}
}
