﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftPlex.Contracts.Request;
using SoftPlex.Contracts.Response;
using SoftPlex.Shared.Response;
using SoftPlex.WebApp.Services;

namespace SoftPlex.WebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly ClientApiProductService _clientApiProductService;

		public ProductController(ClientApiProductService clientApiProductService)
		{
			_clientApiProductService = clientApiProductService;
		}

		public async Task<IActionResult> Index()
		{
			//todo: add async
			IEnumerable<ResponseProduct> res = await _clientApiProductService.GetProducts();
			return View(res);
		}

		public async Task<IActionResult> Detail(Guid id)
		{
			//todo: add async
			ResponseProduct res = await _clientApiProductService.GetProductDetail(id);
			return View(res);
		}

		[Authorize]
		public async Task<IActionResult> RemoveProduct(Guid id)
		{
			//todo: add async
			await _clientApiProductService.RemoveProduct(id);
			return RedirectToAction("Index");
		}

		[Authorize]
		public async Task<IActionResult> RemoveProductVersion(Guid id)
		{
			//todo: add async
			await _clientApiProductService.RemoveProductVersion(id);
			return RedirectToAction("Index");
		}

		
		[HttpPost]
		public async Task<IResult> CreateProduct([FromBody] RequestProduct value)
		{
			RequestProduct request = value;
			request.ProductVersions = request.ProductVersions
				.Where(x =>
					!string.IsNullOrWhiteSpace(x.Name)
					|| !string.IsNullOrWhiteSpace(x.Description)
					|| x.Width > 0 || x.Height > 0 || x.Length > 0
				)
				.ToList();
			;
			
			string json = JsonConvert.SerializeObject(request);
			(bool IsSuccessStatusCode, string Content) res = await _clientApiProductService.CreateProduct(json);
			
			if (res.IsSuccessStatusCode)
			{
				return Results.Json(new { success = "ok", isError = false, url = Url.Action("Index") });
			}
			else
			{
				//Content
				ResponseHasErrors? responseHasErrors = JsonConvert.DeserializeObject<ResponseHasErrors>(res.Content);
				return Results.Json(responseHasErrors);
			}
		}
	}
}
