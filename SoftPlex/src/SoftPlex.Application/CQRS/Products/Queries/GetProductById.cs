using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Queries;

public class GetProductById: IRequest<Result<Product, ErrorList>>
{
	public Guid Id { get; set; }
}