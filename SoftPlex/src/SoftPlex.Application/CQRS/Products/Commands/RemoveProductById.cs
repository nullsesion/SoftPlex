using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Commands;

public class RemoveProductById : IRequest<Result<bool, ErrorList>>
{
	public Guid Id { get; set; }
}