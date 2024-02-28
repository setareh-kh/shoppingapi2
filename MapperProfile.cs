using AutoMapper;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            //mappinq requestDtos to models
            CreateMap<AddUserDto,User>();
            CreateMap<UpdateUserDto,User>();
            CreateMap<AddProductDto,Product>();
            CreateMap<UpdateProductDto,Product>();
            CreateMap<CatogoryDto,Catogory>();
            //mapping Entity models to responsDtos 
            CreateMap<User,UserUserResponseDto>();
            CreateMap<User,AdminUserResponseDto>();
            CreateMap<Product,UserProductResponseDto>();
            CreateMap<Product, AdminProductResponseDto>();
            

        }
    }
}