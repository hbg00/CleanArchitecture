using Application.DTOs.Address;
using Application.DTOs.Common;
using Application.DTOs.Dish;
namespace Application.DTOs.Restaurant
{
    public class RestaurantDto : BaseDto
    {
        public string Category { get; set; }
        public bool HasDelivery { get; set; }

        public AddressDto Address { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}