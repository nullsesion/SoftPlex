﻿using Newtonsoft.Json;
using SoftPlex.Contracts.Response;
using System.Net.Http;
using Microsoft.Extensions.Caching.Distributed;
using ResponseProductVersion = SoftPlex.Contracts.Response.ResponseProductVersion;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace SoftPlex.WebApp.Services
{
	public class ClientApiProductService
	{
		private ClientApiProductCacheService _distributedCache;
		private const string GET_PRODUCTS_POINT = "/api/Product?page=1&pageSize=100";
		private const string GET_PRODUCTS_BY_ID = "/api/Product/";
		private const string DELETE_PRODUCT_POINT = "/api/Product/";
		private const string DELETE_PRODUCTVERSION_POINT = "/api/ProductVersion/";
		private const string GET_FILTER_POINT = "/api/FilterEngine/";
		private const string POST_CREATE_POINT = "/api/Product";
		private IHttpClientFactory _clientFactory { get; set; }
		private HttpClient _httpClient { get; set; }


		public ClientApiProductService(IHttpClientFactory clientFactory
			, ClientApiProductCacheService distributedCache) //, IDistributedCache cache
		{
			_distributedCache = distributedCache;
			_clientFactory = clientFactory;
			_httpClient = _clientFactory.CreateClient(nameof(ClientApiProductService));
			//_serverAviable
		}

		public async Task<List<ResponseFilterEngine>> GetFilter (
			string productNameIn = ""
			,string productVersionNameIn = ""
			,decimal minSize = 0
			,decimal maxSize = 9999_999_999_999
			)
		{
			string getFilterPoint = GET_FILTER_POINT
			 + $"?productNameIn={productNameIn}&productVersionNameIn={productVersionNameIn}&minSize={minSize}&maxSize={maxSize}";

			//_cache.GetAsync()

			HttpResponseMessage response = await _httpClient.GetAsync(getFilterPoint);
			string rawJson = await response.Content.ReadAsStringAsync();

			List<ResponseFilterEngine> listResponseProduct = JsonConvert.DeserializeObject<List<ResponseFilterEngine>>(rawJson);

			return listResponseProduct;
		}

		public async Task<List<ResponseProduct>> GetProducts()
		{
			string rawJson = String.Empty;
			
			string cacheGetProductsKey = "GetProducts";
			Result<string> res = await _distributedCache.TryGetAsync(cacheGetProductsKey);
			if (res.IsFailure || string.IsNullOrWhiteSpace(res.Value))
			{
				HttpResponseMessage response = await _httpClient.GetAsync(GET_PRODUCTS_POINT);
				rawJson = await response.Content.ReadAsStringAsync();
				if(res.IsSuccess)
					_distributedCache.TrySetAsync(cacheGetProductsKey, rawJson);
			}
			else
			{
				rawJson = res.Value;
			}

			List<ResponseProduct> listResponseProduct = JsonConvert.DeserializeObject<List<ResponseProduct>>(rawJson);
			return listResponseProduct;
		}

		public async Task<ResponseProduct> GetProductDetail(Guid id)
		{
			//GetProducts
			/*
			use cache
			*/
			HttpResponseMessage response = await _httpClient.GetAsync( GET_PRODUCTS_BY_ID + id);
			string rawJson = await response.Content.ReadAsStringAsync();

			ResponseProduct? listResponseProduct = JsonConvert.DeserializeObject<ResponseProduct>(rawJson);

			return listResponseProduct;
		}

		public async Task RemoveProduct(Guid id)
		{
			//curl -X 'DELETE' 'https://localhost:7044/api/Product/74e36111-3871-49b0-bc37-5ae965ccd9c2' -H 'accept: */*'
			//DELETE_PRODUCT_POINT
			await _httpClient.DeleteAsync(DELETE_PRODUCT_POINT + id);
		}

		public async Task RemoveProductVersion(Guid id)
		{
			//curl -X 'DELETE' 'https://localhost:7044/api/ProductVersion/d28a85de-88a7-40f8-898f-53784a91b036' -H 'accept: */*'
			//DELETE_PRODUCTVERSION_POINT
			await _httpClient.DeleteAsync(DELETE_PRODUCTVERSION_POINT + id);
		}

		public async Task<(bool IsSuccessStatusCode, string Content)> CreateProduct(string value)
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,  POST_CREATE_POINT);
			StringContent content = new StringContent(value, null, "application/json");
			request.Content = content;
			HttpResponseMessage response = await _httpClient.SendAsync(request);
			
			string responseContent = await response.Content.ReadAsStringAsync();
			bool isSuccessStatusCode = response.IsSuccessStatusCode;

			return (IsSuccessStatusCode: isSuccessStatusCode, Content: responseContent);
		}

	}
	
}
