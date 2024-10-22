namespace SoftPlex.Contracts;

public interface IResponseProductVersion
{
	Guid Id { get; set; }
	Guid ProductId { get; set; }
	string Name { get; set; }
	string Description { get; set; }
	IResponseSizeBox SizeBox { get; set; }
	DateTime CreatingDate { get; set; }
}