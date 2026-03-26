using AutoMapper;
using InventorySystem.Application.DTOs;
using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Sale,SaleDTO>();
            CreateMap<CreateSaleDTO,Sale>();
            CreateMap<RegisterDto, ApplicationUser>()
           .ForMember(dest => dest.UserName,
               opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.PhoneNumber,
               opt => opt.MapFrom(src => src.PhoneNumber));
                }
    
    }
}
