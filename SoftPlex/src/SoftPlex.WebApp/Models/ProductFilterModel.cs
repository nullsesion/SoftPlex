using SoftPlex.Contracts.Response;

namespace SoftPlex.WebApp.Models
{
	public class ProductFilterModel
	{
		public string ProductNameIn { get; set; } = "";
		public string ProductVersionNameIn { get; set; } = "";
		public decimal MinSize { get; set; } = Decimal.Zero;
		public decimal MaxSize { get; set; } = 9999_999_999_999_999;
		public List<ResponseFilterEngine> FilterEngines { get; set; } = new List<ResponseFilterEngine>();
	}
}
