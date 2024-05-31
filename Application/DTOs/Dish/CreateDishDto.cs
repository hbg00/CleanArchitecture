using Application.DTOs.Common;

namespace Application.DTOs.Dish
{
    public class CreateDishDto : CreateBaseDto
    {
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}