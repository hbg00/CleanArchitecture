using RestaurantUI.Model.Address;
using RestaurantUI.Model.Common;
using RestaurantUI.Model.Dish;

namespace RestaurantUI.Model.Restaurant
{
    public class RestaurantVM : BaseVM
    {
        public string Category { get; set; }
        public bool HasDelivery { get; set; }

        public AddressVM Address { get; set; }
        public List<DishVM> Dishes { get; set; }
    }
}
