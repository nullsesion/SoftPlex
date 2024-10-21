using SoftPlex.Domain.ValueObject;
using CSharpFunctionalExtensions;

namespace SoftPlex.Domain
{
	public class ProductVersion
	{
		public const int MAX_TITLE_LENGHT = 255;
		public const int MAX_DESCRIPTION_LENGHT = 1024;

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

		public static Result<ProductVersion> Create(
			Guid productVersionId
			, Guid productId
			, string name
			, string? description
			, SizeBox sizeBox
			, DateTime creatingDate
			)
		{
			if (string.IsNullOrWhiteSpace(name))
				return Result.Failure<ProductVersion>("name must not be empty");

			if (name.Length <= MAX_TITLE_LENGHT)
				return Result.Failure<ProductVersion>("maximum name length exceeded");

			if (description is not null && description.Length <= MAX_DESCRIPTION_LENGHT)
				return Result.Failure<ProductVersion>("maximum description length exceeded");

			return Result.Success(new ProductVersion(
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
