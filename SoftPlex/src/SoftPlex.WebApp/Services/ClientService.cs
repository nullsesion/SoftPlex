using Newtonsoft.Json;
using SoftPlex.Contracts;

using ResponseProductVersion = SoftPlex.Contracts.ResponseProductVersion;

namespace SoftPlex.WebApp.Services
{
	public class ClientService
	{
		private readonly HttpClient _client;

		private const string GET_PRODUCTS_POINT = "https://localhost:7044/api/Product?page=1&pageSize=100";

		public ClientService()
		{
			_client = new HttpClient();
		}

		public async Task<List<ResponseProduct>> GetProducts()
		{
			HttpResponseMessage response = await _client.GetAsync(GET_PRODUCTS_POINT);
			string rawJson = await response.Content.ReadAsStringAsync();

			List<ResponseProduct> listResponseProduct = JsonConvert.DeserializeObject<List<ResponseProduct>>(rawJson);
			
			return listResponseProduct;
		}
	}

}
