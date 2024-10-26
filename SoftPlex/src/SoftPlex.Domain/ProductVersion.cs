using SoftPlex.Domain.ValueObject;
using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Domain
{
	public class ProductVersion
	{
		public const int MAX_TITLE_LENGHT = 255;

		public Guid Id { get; private set; }
		public Guid ProductId { get; private set; }
		public string Name { get; private set; }
		public string? Description { get; private set; }
		public SizeBox SizeBox { get; private set; }
		public DateTime CreatingDate { get; private set; }

		private ProductVersion(
			Guid id
			, Guid productId
			, string name
			, string? description
			, SizeBox sizeBox
			, DateTime creatingDate
		)
		{
			Id = id;
			ProductId = productId;
			Name = name;
			Description = description;
			SizeBox = sizeBox;
			CreatingDate = creatingDate;
		}

		public static Result<ProductVersion, ErrorList> Create(
			Guid productVersionId
			, Guid productId
			, string name
			, string? description
			, SizeBox sizeBox
			, DateTime creatingDate
			)
		{
			ErrorList errorList = new ErrorList();
			if (string.IsNullOrWhiteSpace(name))
				errorList.AddError(new Error("must not be empty",ErrorType.Validation, nameof(Name),productId));
				

			if (name.Length >= MAX_TITLE_LENGHT)
				errorList.AddError(new Error("maximum length exceeded", ErrorType.Validation, nameof(Name), productId));

			if (name.Contains("404"))
				errorList.AddError(new Error("contains 404", ErrorType.Validation, nameof(Name), productId));


			if (sizeBox is null)
				errorList.AddError(new Error("invalid Size", ErrorType.Validation, nameof(SizeBox), productId));

			if (errorList.IsError)
				return Result.Failure<ProductVersion, ErrorList>(errorList);

			return Result.Success<ProductVersion, ErrorList>(new ProductVersion(
				productVersionId
				, productId
				, name
				, description
				, sizeBox
				, creatingDate
				));
		}

		//for ef
		private ProductVersion() { }
	}
}
