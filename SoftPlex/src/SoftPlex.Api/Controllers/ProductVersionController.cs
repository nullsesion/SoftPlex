using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Application.CQRS.ProductVersion.Commands;

namespace SoftPlex.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductVersionController
	{
		private readonly IMediator _mediator;

		public ProductVersionController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpDelete("{id}")]
		public async void Delete(Guid id, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new RemoveProductVersionById()
			{
				Id = id
			}, cancellationToken);
		}
	}
}
