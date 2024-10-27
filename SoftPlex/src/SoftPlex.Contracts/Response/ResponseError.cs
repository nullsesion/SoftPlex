using SoftPlex.Domain.Shared;

namespace SoftPlex.Shared.Response
{
	public class ResponseError
	{
		public string Message { get; set; }
		public ErrorType Type { get; set; }
		public string? InvalidField { get; set; }
		public Guid? EntityId { get; set; }
	}
}
