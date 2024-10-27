using SoftPlex.Contracts.Response;

namespace SoftPlex.WebApp.Models
{
	public class FilterModel
	{
		public ProductFilterModel ProductFilterModel { get; set; }
		public List<ResponseFilterEngine> ListResponseFilterEngine { get; set; }
	}
}
