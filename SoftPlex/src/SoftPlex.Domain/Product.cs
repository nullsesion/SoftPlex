using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Domain
{
	public class Product
	{
		public const int MAX_NAME_LENGHT = 255;

		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string? Description { get; private set; }
		private List<ProductVersion>? _productVersions;
		public IReadOnlyList<ProductVersion> ProductVersions => _productVersions;

		private Product(Guid id
			, string name
			, string? description
			, List<ProductVersion> productVersions)
		{
			Id = id;
			Name = name;
			Description = description;
			_productVersions = productVersions;
		}

		public void UpdateProductVersions(List<ProductVersion> productVersions) 
			=> _productVersions = productVersions;

		//todo add ,Error
		public static Result<Product, ErrorList> Create(Guid Id 
			, string name
			, string? description
			, IEnumerable<ProductVersion>? productVersions)
		{
			ErrorList errorList = new ErrorList();
			if (string.IsNullOrWhiteSpace(name))
				errorList.AddError(new Error("must not be empty", ErrorType.Validation, "Name"));

			if (name.Length > MAX_NAME_LENGHT)
				errorList.AddError(new Error("maximum length exceeded",ErrorType.Validation,"Name"));

			//for debug
			if (name.Contains("404"))
				errorList.AddError(new Error("name contains 404", ErrorType.Validation, "Name"));

			if (description is not null && description.Contains("404"))
				errorList.AddError(new Error("name contains 404", ErrorType.Validation, "Name"));

			if (errorList.IsError)
				return Result.Failure<Product, ErrorList>(errorList);

			return Result.Success<Product, ErrorList>(new Product(
				Id
				, name
				, description
				, productVersions?.ToList() ?? new List<ProductVersion>()));
		}
	}
}
