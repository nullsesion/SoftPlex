using CSharpFunctionalExtensions;
using SoftPlex.Domain;
using MediatR;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateOrUpdateProduct : IRequest<Result<bool,ErrorList>>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public IEnumerable<Domain.ProductVersion> ProductVersions { get; set; }
	}
}
