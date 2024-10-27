using Newtonsoft.Json;
using SoftPlex.Contracts.Response;
using System.Net.Http;
using ResponseProductVersion = SoftPlex.Contracts.Response.ResponseProductVersion;

namespace SoftPlex.WebApp.Services
{
	public class ClientService
	{
		private readonly HttpClient _client;

		private const string GET_PRODUCTS_POINT = "https://localhost:7044/api/Product?page=1&pageSize=100";
		private const string GET_PRODUCTS_BY_ID = "https://localhost:7044/api/Product/";  //9382c4ec-4642-40c8-950d-c302ecfcd51c
		private const string DELETE_PRODUCT_POINT = "https://localhost:7044/api/Product/"; //74e36111-3871-49b0-bc37-5ae965ccd9c2
		private const string DELETE_PRODUCTVERSION_POINT = "https://localhost:7044/api/ProductVersion/"; //d28a85de-88a7-40f8-898f-53784a91b036
		private const string GET_FILTER_POINT = "https://localhost:7044/api/FilterEngine/"; //https://localhost:7044/api/FilterEngine?productNameIn=%D0%94%D0%B5&productVersionNameIn=%D1%81%D1%82%D0%B0&minSize=0&maxSize=10000000000000000
		private const string POST_CREATE_POINT = "https://localhost:7044/api/Product";
		//private const string POST_CREATE_POINT = "https://webhook.site/091dc984-a9b5-40bf-bea0-44a9520129ab";
		//curl -X 'POST' 'https://localhost:7044/api/Product' -H 'accept: */*' -H 'Content-Type: application/json' -d '"string"'

		public ClientService()
		{
			_client = new HttpClient();
		}

		public async Task<List<ResponseFilterEngine>> GetFilter (
			string productNameIn = ""
			,string productVersionNameIn = ""
			,decimal minSize = 0
			,decimal maxSize = 9999_999_999_999
			)
		{

			HttpResponseMessage response = await _client.GetAsync(GET_FILTER_POINT
			+ $"?productNameIn={productNameIn}&productVersionNameIn={productVersionNameIn}&minSize={minSize}&maxSize={maxSize}");
			string rawJson = await response.Content.ReadAsStringAsync();

			List<ResponseFilterEngine> listResponseProduct = JsonConvert.DeserializeObject<List<ResponseFilterEngine>>(rawJson);

			return listResponseProduct;
		}

		public async Task<List<ResponseProduct>> GetProducts()
		{
			HttpResponseMessage response = await _client.GetAsync(GET_PRODUCTS_POINT);
			string rawJson = await response.Content.ReadAsStringAsync();

			List<ResponseProduct> listResponseProduct = JsonConvert.DeserializeObject<List<ResponseProduct>>(rawJson);
			
			return listResponseProduct;
		}

		public async Task<ResponseProduct> GetProductDetail(Guid id)
		{
			HttpResponseMessage response = await _client.GetAsync(GET_PRODUCTS_BY_ID + id);
			string rawJson = await response.Content.ReadAsStringAsync();

			ResponseProduct? listResponseProduct = JsonConvert.DeserializeObject<ResponseProduct>(rawJson);

			return listResponseProduct;
		}

		public async Task RemoveProduct(Guid id)
		{
			//curl -X 'DELETE' 'https://localhost:7044/api/Product/74e36111-3871-49b0-bc37-5ae965ccd9c2' -H 'accept: */*'
			//DELETE_PRODUCT_POINT
			await _client.DeleteAsync(DELETE_PRODUCT_POINT + id);
		}

		public async Task RemoveProductVersion(Guid id)
		{
			//curl -X 'DELETE' 'https://localhost:7044/api/ProductVersion/d28a85de-88a7-40f8-898f-53784a91b036' -H 'accept: */*'
			//DELETE_PRODUCTVERSION_POINT
			await _client.DeleteAsync(DELETE_PRODUCTVERSION_POINT + id);
		}

		public async Task<(bool IsSuccessStatusCode, string Content)> CreateProduct(string value)
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, POST_CREATE_POINT);
			StringContent content = new StringContent(value, null, "application/json");
			request.Content = content;
			HttpResponseMessage response = await _client.SendAsync(request);
			
			string responseContent = await response.Content.ReadAsStringAsync();
			bool isSuccessStatusCode = response.IsSuccessStatusCode;

			return (IsSuccessStatusCode: isSuccessStatusCode, Content: responseContent);
		}

	}

}
