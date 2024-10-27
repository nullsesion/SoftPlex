namespace SoftPlex.Contracts.Request
{
	public class RequestProduct
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public IEnumerable<RequestProductVersion> ProductVersions { get; set; }
	}
}
