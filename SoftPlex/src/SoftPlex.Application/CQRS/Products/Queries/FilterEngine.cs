using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Queries;

public class FilterEngine: IRequest<Result<List<FilterEngineDomain>, ErrorList>>
{
	public string ProductNameIn { get; set; }
	public string ProductVersionNameIn { get; set; }
	public decimal MinSize { get; set; }
	public decimal MaxSize { get; set; }
}