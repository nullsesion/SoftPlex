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
		public static Result<Product> Create(Guid Id 
			, string name
			, string? description
			, IEnumerable<ProductVersion>? productVersions)
		{
			if (string.IsNullOrWhiteSpace(name))
				return Result.Failure<Product>("name must not be empty"); //, Error

			if (name.Length > MAX_NAME_LENGHT)
				return Result.Failure<Product>("maximum name length exceeded");

			//if (description is not null && description.Length <= MAX_DESCRIPTION_LENGHT) return Result.Failure<Product>("maximum description length exceeded");

			return Result.Success<Product>(new Product(
				Id
				, name
				, description
				, productVersions?.ToList() ?? new List<ProductVersion>()));
		}
	}
}
