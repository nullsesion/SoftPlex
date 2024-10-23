﻿using AutoMapper;
using SoftPlex.Domain.ValueObject;
using SoftPlex.Contracts;
using SoftPlex.Domain;

namespace SoftPlex.Api.MapperConfig
{
	public partial class AppMappingProfile : Profile
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
	}
}
