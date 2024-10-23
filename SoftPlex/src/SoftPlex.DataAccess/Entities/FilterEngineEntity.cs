namespace SoftPlex.DataAccess.Entities
{
	public class FilterEngineEntity
	{
		Guid Uuid { get; set; }
		string ProductName { get; set; }
		string ProductVersionName { get; set; }
		decimal Width { get; set; }
		decimal Height { get; set; }
		private decimal Length { get; set; }
	}
}
