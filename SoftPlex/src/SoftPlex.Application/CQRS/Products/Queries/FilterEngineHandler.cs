using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class FilterEngineHandler: IRequestHandler<FilterEngine, Result<List<FilterEngineDomain>, ErrorList>>
	{
		private readonly IProductRepository _productRepository;

		public FilterEngineHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result<List<FilterEngineDomain>, ErrorList>> Handle(FilterEngine request, CancellationToken cancellationToken)
		{
			return _productRepository.GetFromFilterEngine(request.ProductNameIn
				, request.ProductVersionNameIn
				, request.MinSize
				, request.MaxSize
				);
		}
	}
}
