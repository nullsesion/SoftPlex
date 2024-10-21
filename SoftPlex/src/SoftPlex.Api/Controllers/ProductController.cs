using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Application.CQRS.Products.Queries;
using SoftPlex.Domain;
using IResult = Microsoft.AspNetCore.Http.IResult;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftPlex.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/<ProductController>
		[HttpGet]
		public async Task<IResult> Get(int page, int pageSize, CancellationToken cancellationToken)
		{
			Result<IReadOnlyList<Product>> result = await _mediator.Send(new GetProducts()
			{
				Page = page, PageSize = pageSize
			}, cancellationToken);
			if (result.IsSuccess)
			{
				return Results.Json(result.Value);
			}
			//todo: разделить серверные и пользовательские ошибки
			return Results.BadRequest(result.Error);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<ProductController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/*
		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}
		*/

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
