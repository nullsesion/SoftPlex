﻿using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Application.CQRS.Products.Commands;
using SoftPlex.Application.CQRS.Products.Queries;
using SoftPlex.Domain;
using SoftPlex.Domain.ValueObject;
using IResult = Microsoft.AspNetCore.Http.IResult;
using SoftPlex.Contracts.Request;
using SoftPlex.Contracts.Response;
using Newtonsoft.Json;
using SoftPlex.Domain.Shared;
using SoftPlex.Application.DtoModels;

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
			var result = await _mediator.Send(new GetProducts()
			{
				Page = page, PageSize = pageSize
			}, cancellationToken);
			if (result.IsFailure)
			{
				ErrorList? errs = result.Error;
				if(errs is not null)
					return Results.BadRequest(errs);

				return Results.BadRequest(new Error("unknown error",ErrorType.ServerError,null,null));
			}

			//if (result.IsSuccess) { }
			IEnumerable<ResponseProduct> resp = result.Value
					.Select(x => _mapper.Map<ResponseProduct>(x))
				;
			return Results.Json(resp);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public async Task<IResult> Get(Guid id, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetProductById()
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
		public async Task<IResult> Post([FromBody] RequestProduct value, CancellationToken cancellationToken)
		{
			RequestProductDTO productDTO = _mapper.Map<RequestProductDTO>(value);

			CreateOrUpdateProduct createOrUpdateProduct = new CreateOrUpdateProduct()
			{
				Product = productDTO
			};
			
			var result = await _mediator.Send(createOrUpdateProduct, cancellationToken);
			if (result.IsFailure)
			{
				return Results.BadRequest(result.Error);
			}
				
			
			return Results.Json(new { success = "ok" });
			/*
			RequestProductVersion t = value.ProductVersions.ToList().First();
			ProductVersionDto? res = _mapper.Map<ProductVersionDto>(t);
			return Results.Json(res);
			*/
			/*
			Guid productGuid = value.Id == Guid.Empty ? Guid.NewGuid() : value.Id;
			List<ProductVersion> productVersions = new List<ProductVersion>();
			foreach (RequestProductVersion item in value.ProductVersions)
			{
				Guid productVersionId = item.Id == Guid.Empty?Guid.NewGuid() : item.Id;
				Result<SizeBox, ErrorList> trySizeBox = SizeBox.Create(item.Width, item.Height, item.Length);
				if(trySizeBox.IsFailure)
					continue;

				//item
				Result<ProductVersion, ErrorList> tryProductVersion = ProductVersion.Create(
						productVersionId
						, productGuid
						, item.Name
						, item.Description
						, trySizeBox.Value
						,  DateTime.Now
					);

				if(tryProductVersion.IsSuccess)
					productVersions.Add(tryProductVersion.Value);
			}

			CreateOrUpdateProduct createOrUpdateProduct = new CreateOrUpdateProduct()
			{
				Id = productGuid,
				Name = value.Name,
				Description = value.Description,
				ProductVersions = productVersions,
			};

			var result = await _mediator.Send(createOrUpdateProduct, cancellationToken);
			if (result.IsFailure)
				return Results.BadRequest(result.Error); 
			*/
			return Results.Json(new { success = "ok" });
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
