namespace SoftPlex.DataAccess.Entities
{
	public class ProductEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		
		public IEnumerable<ProductVersionEntity>? ProductVersionEntities { get; set; }
	}
}
