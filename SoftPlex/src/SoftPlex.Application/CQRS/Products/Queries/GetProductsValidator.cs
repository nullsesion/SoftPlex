using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProductsValidator : AbstractValidator<GetProducts>
	{
		public GetProductsValidator()
		{
			RuleFor(x => x.Page)
				.Must(x => x > 0)
				.WithMessage("Invalid Page");

			RuleFor(x => x.PageSize)
				.Must(x => x > 0)
				.WithMessage("Invalid PageSize");
		}
	}
}
