using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries;

public class GetProductById: IRequest<Result<Product>>
{
	public Guid Id { get; set; }
}