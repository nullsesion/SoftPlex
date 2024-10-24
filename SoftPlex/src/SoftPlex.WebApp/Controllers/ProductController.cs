using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

		[Authorize]
		[HttpPost]
		public async Task<IResult> CreateProduct()
		{
			//todo: add async
			//await _clientService.CreateProduct();
			
			return Results.Json(new {success = "ok"});
		}
	}
}
