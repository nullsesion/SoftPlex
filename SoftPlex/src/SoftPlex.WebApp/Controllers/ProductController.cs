using Microsoft.AspNetCore.Mvc;
using SoftPlex.Contracts;
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
	}
}
