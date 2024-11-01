using Microsoft.AspNetCore.Mvc;
using SoftPlex.Contracts.Response;
using SoftPlex.WebApp.Models;
using SoftPlex.WebApp.Services;
using System;

namespace SoftPlex.WebApp.Controllers
{
	public class ProductFilterController : Controller
	{
		private readonly ClientApiProductService _clientApiProductService;

		public ProductFilterController(ClientApiProductService clientApiProductService)
		{
			_clientApiProductService = clientApiProductService;
		}

		public async Task<IActionResult> Index(string productName = "",string productVersionName = "", string minSize = "", string maxSize = "")
		{
			if (Decimal.TryParse(minSize, out decimal minS))
			{
				minS = minS < 0?0: minS;
			}
			
			if (Decimal.TryParse(maxSize, out decimal maxS))
			{
				maxS = maxS < 0 ? 0 : maxS;
			}
			else
			{
				maxS = 9999_999_999_999_999;
			}

			var model = new ProductFilterModel()
			{
				ProductNameIn = productName
				, ProductVersionNameIn = productVersionName
				, MinSize = minS
				, MaxSize = maxS
			};

			model.FilterEngines = await _clientApiProductService
				.GetFilter(productName
					, productVersionName
					, minS
					, maxS
				);
			
			return View(model);
		}

	}
}
