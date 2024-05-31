using RestaurantUI.Model.Address;

namespace RestaurantUI.Model.Restaurant
{
    public class RestaurantUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public AddressVM Address { get; set; }
    }
}
