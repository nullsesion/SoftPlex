using AutoMapper;
using SoftPlex.Contracts;
using System.Runtime;
using SoftPlex.Domain.ValueObject;
using SoftPlex.Api.Models;
using SoftPlex.Domain;

namespace SoftPlex.Api.MapperConfig
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<SizeBox, ResponseSizeBox>();
			CreateMap<ProductVersion,ResponseProductVersion>()
				.ForMember(dest => dest.SizeBox
					, opt 
						=> opt.MapFrom(src => new ResponseSizeBox() { Height = src.SizeBox.Height, Width = src.SizeBox.Width, Length = src.SizeBox.Length }
					))
			;

			CreateMap<Product, ResponseProduct>()
				.ForMember(dest => dest.ProductVersions
					, opt 
						=> opt.MapFrom( src => ProductToResponseProduct(src)))
				;
		}

		private IEnumerable<ResponseProductVersion> ProductToResponseProduct(Product product)
		{
			return product
				.ProductVersions
				.Select(x 
					=> new ResponseProductVersion()
						{
							Id = x.Id
							, ProductId = x.ProductId
							, Name = x.Name
							, Description = x.Description
							, SizeBox = new ResponseSizeBox() { Height = x.SizeBox.Height, Width = x.SizeBox.Width, Length = x.SizeBox.Length }
							, CreatingDate = DateTime.Now
						}
					);
		}
	}
}
