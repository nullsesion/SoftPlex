using Newtonsoft.Json;

namespace SoftPlex.Domain.Shared
{
	public record Error(string Message
		, ErrorType Type
		, string? InvalidField
		, Guid? EntityId)
	{
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public Error? Deserialize(string value)
		{
			return JsonConvert.DeserializeObject<Error>(value);
		}
	};
}
