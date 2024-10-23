using CSharpFunctionalExtensions;
using MediatR;

namespace SoftPlex.Application.CQRS.Products.Commands;

public class RemoveProductById : IRequest<Result>
{
	public Guid Id { get; set; }
}