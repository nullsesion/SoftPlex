using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;


namespace SoftPlex.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(
			this IServiceCollection services)
		{
			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}
	}
}
