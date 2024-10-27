namespace SoftPlex.Contracts.Request
{
	public class RequestProductVersion
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Length { get; set; }
	}
}
