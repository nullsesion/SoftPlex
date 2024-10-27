using Microsoft.AspNetCore.Mvc;
using SoftPlex.Contracts.Response;
using SoftPlex.WebApp.Models;
using SoftPlex.WebApp.Services;
using System;

namespace SoftPlex.WebApp.Controllers
{
	public class ProductFilterController : Controller
	{
		private readonly ClientService _clientService;

		public ProductFilterController(ClientService clientService)
		{
			_clientService = clientService;
		}

		/*
		public async Task<IActionResult> Index()
		{
			List<ResponseFilterEngine> res = await _clientService
				.GetFilter(""
					, ""
					, 0
					, 9999_999_999_999_999
				);

			FilterModel model = new FilterModel();
			return View(model);
		}
		*/

		public async Task<IActionResult> Index()
		{
			string ProductNameIn = Request?.Query["ProductName"] ?? "";
			string ProductVersionNameIn = Request?.Query["ProductVersionName"] ?? "";
			decimal MinSize = 0;
			decimal MaxSize = 9999_999_999_999_999;
			if (Request?.Query["MinSize"] is not null)
			{
				if (Decimal.TryParse(Request?.Query["MinSize"], out decimal minS))
				{
					MinSize = minS < 0?0: minS;
				}
			}
			if (Request?.Query["MaxSize"] is not null)
			{
				if (Decimal.TryParse(Request?.Query["MaxSize"], out decimal maxS))
				{
					MaxSize = maxS < 0 ? 0 : maxS;
				}
			}


			List<ResponseFilterEngine> res = await _clientService
				.GetFilter(ProductNameIn
					, ProductVersionNameIn
					, MinSize
					, MaxSize
				);

			
			return View(res);
			
		}
	}
}
