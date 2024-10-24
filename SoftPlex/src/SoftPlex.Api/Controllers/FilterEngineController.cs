using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Application.CQRS.Products.Queries;
using SoftPlex.Contracts;
using SoftPlex.Contracts.Response;
using SoftPlex.Domain;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace SoftPlex.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilterEngineController
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public FilterEngineController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		//todo: сделать несколько хранимок в зависимости от параметров
		[HttpGet]
		public async Task<IResult> Get(
			string productNameIn = ""
			, string productVersionNameIn = ""
			, decimal minSize = Decimal.Zero
			, decimal maxSize = 9999_999_999_999_999
			
			)
		{
			/*
			productNameIn
			productVersionNameIn
			*/
			if (string.IsNullOrWhiteSpace(productNameIn))
				productNameIn = "";

			if (string.IsNullOrWhiteSpace(productVersionNameIn))
				productVersionNameIn = "";

			Result<List<FilterEngineDomain>> result = await _mediator.Send(new FilterEngine()
			{
				ProductNameIn = productNameIn,
				ProductVersionNameIn = productVersionNameIn,
				MinSize = minSize,
				MaxSize = maxSize
			});

			// 

			if (result.IsSuccess)
			{
				return Results.Json(result.Value.Select(x => new ResponseFilterEngine()
				{
					Id = x.Id,
					ProductName = x.ProductName,
					ProductVersionName = x.ProductVersionName,
					Width = x.Width,
					Height = x.Height,
					Length = x.Length,
				}));
			}
			
			//todo: разделить серверные и пользовательские ошибки
			return Results.BadRequest(result.Error);
			
		}
	}
}
