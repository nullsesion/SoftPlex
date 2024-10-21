using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateProductOrUpdateHandler : IRequestHandler<CreateOrUpdateProduct, Result>
	{
		private readonly IProductRepository _productRepository;

		public CreateProductOrUpdateHandler(IProductRepository productRepository) 
			=> _productRepository = productRepository;

		public Task<Result> Handle(CreateOrUpdateProduct request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
			//_productRepository.InsertOrUpdateProductAsync(_productRepository)
		}
	}
}
