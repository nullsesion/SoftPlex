using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProducts: IRequest<Result<IReadOnlyList<Product>>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
