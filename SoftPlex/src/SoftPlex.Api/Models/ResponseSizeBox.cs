using SoftPlex.Contracts;

namespace SoftPlex.Api.Models
{
	public class ResponseSizeBox: IResponseSizeBox
	{
		/*
		public ResponseSizeBox(decimal width, decimal height, decimal length)
			=> (Width, Height, Length) = (width, height, length);
		*/
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Length { get; set; }
	}
}
