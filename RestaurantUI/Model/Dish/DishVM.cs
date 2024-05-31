using RestaurantUI.Model.Common;

namespace RestaurantUI.Model.Dish
{
    public class DishVM : BaseVM
    {
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
