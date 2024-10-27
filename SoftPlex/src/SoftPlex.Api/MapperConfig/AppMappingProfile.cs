using AutoMapper;
using SoftPlex.Domain.ValueObject;
using SoftPlex.Domain;
using SoftPlex.Contracts.Response;
using SoftPlex.Contracts.Request;
using SoftPlex.Application.CQRS.Products.Commands;
using SoftPlex.Application.DtoModels;

namespace SoftPlex.Api.MapperConfig
{
	public partial class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{

			CreateMap<RequestProduct, RequestProductDTO>()
				.ForCtorParam(nameof(RequestProductDTO.Id)
					, options => options.MapFrom(rpv => rpv.Id))
				.ForCtorParam(nameof(RequestProductDTO.Name)
					, options => options.MapFrom(rpv => rpv.Name))
				.ForCtorParam(nameof(RequestProductDTO.Description)
					, options => options.MapFrom(rpv => rpv.Description))
				.ForCtorParam(nameof(RequestProductDTO.ProductVersions)
					, options => options.MapFrom(rpv => rpv.ProductVersions))

				;

			CreateMap<RequestProductVersion, ProductVersionDto>()
				//.ForMember(x => x.IsNew, opt => opt.Ignore())
				.ForCtorParam(nameof(ProductVersionDto.Id)
				, options => options.MapFrom(rpv => rpv.Id))
				.ForCtorParam(nameof(ProductVersionDto.ProductId)
				, options => options.MapFrom(rpv => rpv.ProductId))
				.ForCtorParam(nameof(ProductVersionDto.Name)
					, options => options.MapFrom(rpv => rpv.Name))
				.ForCtorParam(nameof(ProductVersionDto.Description)
					, options => options.MapFrom(rpv => rpv.Description))
				.ForCtorParam(nameof(ProductVersionDto.Width)
					, options => options.MapFrom(rpv => rpv.Width))
				.ForCtorParam(nameof(ProductVersionDto.Height)
					, options => options.MapFrom(rpv => rpv.Height))
				.ForCtorParam(nameof(ProductVersionDto.Length)
					, options => options.MapFrom(rpv => rpv.Length))
				;


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

			CreateMap<ResponseFilterEngine, FilterEngineDomain>();
			CreateMap<FilterEngineDomain, ResponseFilterEngine>();

		}
	}
}
