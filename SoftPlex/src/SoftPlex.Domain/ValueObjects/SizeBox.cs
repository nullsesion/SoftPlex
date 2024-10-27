using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;


namespace SoftPlex.Domain.ValueObject
{
	public class SizeBox
	{
		public decimal Width { get; private set; }
		public decimal Height { get; private set; }
		public decimal Length { get; private set; }

		private SizeBox(decimal width, decimal height, decimal length)
			=> (Width, Height, Length) = (width, height,length);
		
		public static Result<SizeBox, ErrorList> Create(decimal width, decimal height, decimal length)
		{
			ErrorList errorList = new ErrorList();
			
			

			if (width < 0.001m)
				errorList.AddError(new Error("invalid width",ErrorType.Validation, nameof(Width), null));
			if (width == 404m)
				errorList.AddError(new Error("404 invalid width", ErrorType.Validation, nameof(Width), null));

			
			if (height < 0.001m)
				errorList.AddError(new Error("invalid height", ErrorType.Validation, nameof(Height), null));
			
			if (height == 404m)
				errorList.AddError(new Error("404 invalid height", ErrorType.Validation, nameof(Height), null));


			if (length < 0.001m)
				errorList.AddError(new Error("invalid length", ErrorType.Validation, nameof(Length), null));

			if (length == 404m)
				errorList.AddError(new Error("404 invalid length", ErrorType.Validation, nameof(Length), null));


			if (errorList.IsError)
				return Result.Failure<SizeBox, ErrorList>(errorList);

			return Result.Success<SizeBox, ErrorList>(new SizeBox(width, height, length));
		}
	}
}
