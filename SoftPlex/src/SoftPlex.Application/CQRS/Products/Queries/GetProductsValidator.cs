using FluentValidation;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProductsValidator : AbstractValidator<GetProducts>
	{
		public GetProductsValidator()
		{
			RuleFor(getProducts => getProducts.Page)
				.GreaterThanOrEqualTo(1);
			RuleFor(getProducts => getProducts.PageSize)
				.GreaterThanOrEqualTo(1);
		}
	}
}
