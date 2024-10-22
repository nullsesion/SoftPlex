using SoftPlex.Contracts;

namespace SoftPlex.Api.Models
{
	public class ResponseProduct: IResponseProduct
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<IResponseProductVersion> ProductVersions { get; set; }
	}
}
