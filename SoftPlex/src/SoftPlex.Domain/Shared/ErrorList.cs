namespace SoftPlex.Domain.Shared
{
	public class ErrorList
	{
		private readonly List<Error> _errors = new List<Error>(); 
		public bool IsError { get; private set; } = false;
		public IReadOnlyList<Error> Errors => _errors;

		public void AddError(Error error)
		{
			IsError = true;
			_errors.Add(error);
		}
		public void AddErrors(IEnumerable<Error> error)
		{
			IsError = true;
			_errors.Concat(error);
		}
	}
}
