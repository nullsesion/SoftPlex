using CSharpFunctionalExtensions;
using SoftPlex.Domain;
using MediatR;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateOrUpdateProduct : IRequest<Result>
	{
		public Guid ProductId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public IEnumerable<ProductVersion> ProductVersions { get; set; }
	}
}
