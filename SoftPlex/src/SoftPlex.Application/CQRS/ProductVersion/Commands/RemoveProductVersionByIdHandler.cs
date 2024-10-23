using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.CQRS.Products.Commands;
using SoftPlex.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.CQRS.ProductVersion.Commands
{
	public class RemoveProductVersionByIdHandler : IRequestHandler<RemoveProductVersionById, Result>
	{
		private readonly IProductRepository _productRepository;

		public RemoveProductVersionByIdHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Result> Handle(RemoveProductVersionById request, CancellationToken cancellationToken)
		{
			return await _productRepository.RemoveProductVersionByIdAsync(request.Id, cancellationToken);
		}
	}
}
