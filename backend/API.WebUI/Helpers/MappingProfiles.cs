using API.Core.Entities;
using API.WebUI.DTOs;
using AutoMapper;

namespace API.WebUI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))  //ProductBrand class to string
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));  //ProductType class to string
        }
    }
}
