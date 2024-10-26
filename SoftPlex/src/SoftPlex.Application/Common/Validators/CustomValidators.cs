using CSharpFunctionalExtensions;
using FluentValidation;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;
using Newtonsoft.Json;

namespace SoftPlex.Application.Common.Validators
{
	public static class CustomValidators
	{
		public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
			this IRuleBuilder<T, TElement> ruleBuilder,
			Func<TElement, Result<TValueObject, ErrorList>> factoryMethod)
		{
			return ruleBuilder.Custom((value, context) =>
			{
				Result<TValueObject, ErrorList> result = factoryMethod(value);

				if (result.IsSuccess)
					return;

				context.AddFailure(JsonConvert.SerializeObject(result.Error.Errors));

			});
		}

		public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
			this IRuleBuilderOptions<T, TProperty> ruleBuilder, ErrorList error)
		{
			return ruleBuilder.WithMessage(JsonConvert.SerializeObject(error.Errors));
		}
	}
}