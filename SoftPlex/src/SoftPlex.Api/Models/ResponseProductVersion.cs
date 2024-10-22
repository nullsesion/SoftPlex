using SoftPlex.Contracts;

namespace SoftPlex.Api.Models
{
	public class ResponseProductVersion: IResponseProductVersion
	{
		/* 
		public ResponseProductVersion(Guid id, Guid productId, string name, string description, IResponseSizeBox responseSizeBox, DateTime creatingDate)
		{
			Id = id;
			ProductId = productId;
			Name = name;
			Description = description;
			ResponseSizeBox = responseSizeBox;
			CreatingDate = creatingDate;
		}
		*/
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IResponseSizeBox SizeBox { get; set; }
		public DateTime CreatingDate { get; set; }
	}
}
