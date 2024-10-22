using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Api.Models;
using SoftPlex.Application.CQRS.Products.Queries;
using SoftPlex.Domain;
using SoftPlex.Domain.ValueObject;
using System.Linq;
using IResult = Microsoft.AspNetCore.Http.IResult;

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
				
				var request = result.Value
					.Select(x => _mapper.Map<ResponseProduct>(x))
					;
				
				/*
				var request = result.Value
					.SelectMany(x => x.ProductVersions)
					.Select(i => _mapper.Map<ResponseProductVersion>(i))
				;
				*/
				/*
				var request = result.Value
						//.Select(x => x.ProductVersions.Select(i => i.SizeBox))
						.SelectMany(x => x.ProductVersions)
						.Select(i => _mapper.Map<ResponseSizeBox>(i.SizeBox))
					;
				*/

				//_mapper.Map<ResponseProduct>(x)
				//_mapper.Map<CityResponse>(response.Value)

				return Results.Json(request);
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
