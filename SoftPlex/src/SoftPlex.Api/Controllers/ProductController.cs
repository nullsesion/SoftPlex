using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Application.CQRS.Products.Commands;
using SoftPlex.Application.CQRS.Products.Queries;
using SoftPlex.Domain;
using SoftPlex.Domain.ValueObject;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using IResult = Microsoft.AspNetCore.Http.IResult;
using SoftPlex.Application.CQRS.ProductVersion.Commands;
using SoftPlex.Contracts.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftPlex.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public ProductController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
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
				
				IEnumerable<ResponseProduct> resp = result.Value
					.Select(x => _mapper.Map<ResponseProduct>(x))
					;

				return Results.Json(resp);
			}

			//todo: разделить серверные и пользовательские ошибки
			return Results.BadRequest(result.Error);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public async Task<IResult> Get(Guid id, CancellationToken cancellationToken)
		{
			Result<Product> result = await _mediator.Send(new GetProductById()
			{
				Id = id
			}, cancellationToken);
			if(result.IsSuccess)
				return Results.Json(result.Value);

			//todo: разделить серверные и пользовательские ошибки
			return Results.BadRequest(result.Error);
		}

		// POST api/<ProductController>
		[HttpPost]
		public async Task<IResult> Post([FromBody] string value, CancellationToken cancellationToken)
		{
			
			Guid pid = new Guid("87efbc55-6c4a-42a0-a1e4-ce8582a27977");
			
			
			Result result = await _mediator.Send(new CreateOrUpdateProduct()
			{
				Id = pid
				, Name = "32423432424"
				, Description = "2343242424"
				, ProductVersions = new List<ProductVersion>()
				{
					ProductVersion.Create(
						Guid.NewGuid()
						, pid
						, "sdsdsdsd"
						, "asasasas"
						, SizeBox.Create(100, 1000, 100).Value
						, DateTime.Now
					).Value
					, ProductVersion.Create(
						Guid.NewGuid()
						, pid
						, "dsfdsfsdf"
						, "adsfdsfsasasas"
						, SizeBox.Create(100, 1000, 100).Value
						, DateTime.Now
					).Value
				}
			}, cancellationToken);

			if (result.IsSuccess)
			{
				return Results.Json(new { success = "ok" });
			}
			return Results.BadRequest(result.Error);
			
			//return Results.Json(value);
		}

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		public async void Delete(Guid id, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new RemoveProductById()
			{
				Id = id
			}, cancellationToken);
		}
	}
}
