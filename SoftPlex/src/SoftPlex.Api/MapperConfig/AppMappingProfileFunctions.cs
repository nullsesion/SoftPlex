using SoftPlex.Contracts;
using SoftPlex.Contracts.Response;
using SoftPlex.Domain;

namespace SoftPlex.Api.MapperConfig
{
	public partial class AppMappingProfile
	{
		private IEnumerable<ResponseProductVersion> ProductToResponseProduct(Product product)
		{
			return product
				.ProductVersions
				.Select(x
					=> new ResponseProductVersion()
					{
						Id = x.Id,
						ProductId = x.ProductId,
						Name = x.Name,
						Description = x.Description
						,
						SizeBox = new ResponseSizeBox() { Height = x.SizeBox.Height, Width = x.SizeBox.Width, Length = x.SizeBox.Length }
						,
						CreatingDate = DateTime.Now
					}
				);
		}
	}
}
