using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;


namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class RemoveProductByIdHandler : IRequestHandler<RemoveProductById, Result>
	{
		private readonly IProductRepository _productRepository;

		public RemoveProductByIdHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result> Handle(RemoveProductById request, CancellationToken cancellationToken)
		{
			return await _productRepository.RemoveProductByIdAsync(request.Id, cancellationToken);
		}
	}
}
