using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Application.DtoModels;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateOrUpdateProduct : IRequest<Result<bool,ErrorList>>
	{
		public RequestProductDTO Product { get; set; }
	}
}
