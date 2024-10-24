namespace SoftPlex.Contracts.Response;

public class ResponseProduct
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public IEnumerable<ResponseProductVersion> ProductVersions { get; set; }
}