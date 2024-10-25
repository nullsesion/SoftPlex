using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftPlex.Contracts.Request;
using SoftPlex.Contracts.Response;
using SoftPlex.WebApp.Services;

namespace SoftPlex.WebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly ClientService _clientService;

		public ProductController(ClientService clientService)
		{
			_clientService = clientService;
		}

		public async Task<IActionResult> Index()
		{
			//todo: add async
			IEnumerable<ResponseProduct> res = await _clientService.GetProducts();
			return View(res);
		}

		public async Task<IActionResult> Detail(Guid id)
		{
			//todo: add async
			ResponseProduct res = await _clientService.GetProductDetail(id);
			return View(res);
		}

		[Authorize]
		public async Task<IActionResult> RemoveProduct(Guid id)
		{
			//todo: add async
			await _clientService.RemoveProduct(id);
			return RedirectToAction("Index");
		}

		[Authorize]
		public async Task<IActionResult> RemoveProductVersion(Guid id)
		{
			//todo: add async
			await _clientService.RemoveProductVersion(id);
			return RedirectToAction("Index");
		}


		/*
		[HttpPost]
		public async Task<IResult> CreateProduct([FromBody] RequestProduct value)
		{
			string json = JsonConvert.SerializeObject(value);
			await _clientService.CreateProduct(json);
			return Results.Json(new {success = "ok"});
		}
		*/
		[HttpPost]
		public async Task<IResult> CreateProduct([FromBody] RequestProduct value)
		{
			string json = JsonConvert.SerializeObject(value);
			await _clientService.CreateProduct(json);
			return Results.Json(new { success = "ok" });
		}
	}
}
