namespace SoftPlex.Domain.Shared
{
	public record Error(string Message
	, ErrorType Type
	, string? InvalidField);
}
