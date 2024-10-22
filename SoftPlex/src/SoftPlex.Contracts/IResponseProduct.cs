namespace SoftPlex.Contracts;

public interface IResponseProduct
{
	Guid Id { get; set; }
	string Name { get; set; }
	string Description { get; set; }
	IEnumerable<IResponseProductVersion> ProductVersions { get; set; }
}