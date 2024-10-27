using System.Collections.Immutable;
using System.Net.WebSockets;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;
using SoftPlex.Domain.ValueObject;

namespace SoftPlex.Application.CQRS.Products.Commands
{
	public class CreateProductOrUpdateValidator : AbstractValidator<CreateOrUpdateProduct>
	{
		public CreateProductOrUpdateValidator()
		{
			RuleFor(rq => rq.Product)
				.Custom((pv, context) =>
				{
					Result<Product, ErrorList> tryProduct = Product.Create(pv.Id,pv.Name,pv.Description, new List<Domain.ProductVersion>() );
					if (tryProduct.IsFailure)
					{
						var name =
							tryProduct.Error.Errors
								.FirstOrDefault(x => x.InvalidField == nameof(pv.Name));
						var description =
							tryProduct.Error.Errors
								.FirstOrDefault(x => x.InvalidField == nameof(pv.Description));
						
						if(name is not null)
							context.AddFailure(new ValidationFailure(name.InvalidField
								, name.Serialize()));

						if (description is not null)
							context.AddFailure(new ValidationFailure(description.InvalidField
								, description.Serialize()));
					}
				});

			RuleForEach(rq => rq.Product.ProductVersions)
				.ChildRules(pv =>
					{

						pv.RuleFor(pv => new
							{
								pv.Id
								, pv.ProductId
								, pv.Name
								, pv.Description
								//, pv.Width
								//, pv.Height
								//, pv.Length

						})
							.Custom((sb, context) =>
							{
								Result<SizeBox, ErrorList> trySizeBox = SizeBox.Create(1, 1,1);
								if (trySizeBox.IsSuccess)
								{
									var tryPv 
										= Domain.ProductVersion.Create(sb.Id
											, Guid.NewGuid()
											, sb.Name
											, sb.Description
											, trySizeBox.Value
											, DateTime.Now);

									if (tryPv.IsFailure)
									{

										var n = tryPv.Error.Errors
											.FirstOrDefault(x => x.InvalidField == nameof(sb.Name));
										var d = tryPv.Error.Errors
											.FirstOrDefault(x => x.InvalidField == nameof(sb.Description));

										if (n is not null)
										{
											var wErr = new Error(n.Message, n.Type, n.InvalidField, sb.Id);
											context.AddFailure(new ValidationFailure(n.InvalidField
												, wErr.Serialize()));
										}

										if (d is not null)
										{
											var wErr = new Error(d.Message, d.Type, d.InvalidField, sb.Id);
											context.AddFailure(new ValidationFailure(d.InvalidField
												, wErr.Serialize()));
										}
									}
								}
								
								
							});

						pv.RuleFor(pv => new {pv.Id, pv.Width, pv.Height, pv.Length } )
							.Custom((sb, context) =>
							{
								Result<SizeBox, ErrorList> trySizeBox = SizeBox.Create(sb.Width, sb.Height, sb.Length);
								if (trySizeBox.IsFailure)
								{
									var w =
										trySizeBox.Error.Errors
											.FirstOrDefault(x => x.InvalidField == nameof(sb.Width));
									var h =
										trySizeBox.Error.Errors
											.FirstOrDefault(x => x.InvalidField == nameof(sb.Height));
									var l =
										trySizeBox.Error.Errors
											.FirstOrDefault(x => x.InvalidField == nameof(sb.Length));

									if (w is not null)
									{
										var wErr = new Error(w.Message, w.Type, w.InvalidField, sb.Id);
										context.AddFailure(new ValidationFailure(w.InvalidField
											, wErr.Serialize()));
									}

									if (h is not null)
									{
										var hErr = new Error(h.Message, h.Type, h.InvalidField, sb.Id);
										context.AddFailure(new ValidationFailure(h.InvalidField
											, hErr.Serialize()));
									}

									if (l is not null)
									{
										var lErr = new Error(l.Message, l.Type, l.InvalidField, sb.Id);
										context.AddFailure(new ValidationFailure(l.InvalidField
											, lErr.Serialize()));
									}
								}
							});
				});
		}
	}
}
