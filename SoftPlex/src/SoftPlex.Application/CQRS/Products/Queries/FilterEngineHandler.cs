using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class FilterEngineHandler: IRequestHandler<FilterEngine, Result<List<FilterEngineDomain>>>
	{
		private readonly IProductRepository _productRepository;

		public FilterEngineHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result<List<FilterEngineDomain>>> Handle(FilterEngine request, CancellationToken cancellationToken)
		{
			//Result<List<FilterEngineDomain>> result =
			return _productRepository.GetFromFilterEngine(request.ProductNameIn, request.ProductVersionNameIn, request.MinSize,
				request.MaxSize);
		}
	}
}
