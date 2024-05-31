using Application.DTOs.Address;
using Application.DTOs.Dish;
using Application.DTOs.Restaurant;
using AutoMapper;
using Domain.Entity;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dish, CreateDishDto>().ReverseMap();
            CreateMap<Restaurant, CreateRestaurantDto>().ReverseMap();

            CreateMap<Dish, UpdateDishDto>().ReverseMap();
            CreateMap<Restaurant, UpdateRestaurantDto>().ReverseMap();

            CreateMap<Dish, DishDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
        }
    }
}