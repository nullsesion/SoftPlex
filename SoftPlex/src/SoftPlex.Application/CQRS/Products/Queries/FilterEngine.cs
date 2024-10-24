using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries;

public class FilterEngine: IRequest<Result<List<FilterEngineDomain>>>
{
	public string ProductNameIn { get; set; }
	public string ProductVersionNameIn { get; set; }
	public decimal MinSize { get; set; }
	public decimal MaxSize { get; set; }
}