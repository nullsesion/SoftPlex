namespace SoftPlex.Contracts.Response;

public class ResponseProductVersion
{
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public ResponseSizeBox SizeBox { get; set; }
	public DateTime CreatingDate { get; set; }
}