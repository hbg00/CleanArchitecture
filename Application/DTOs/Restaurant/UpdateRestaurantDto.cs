using Application.DTOs.Address;

namespace Application.DTOs.Restaurant
{
    public class UpdateRestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public AddressDto Address { get; set; }
    }
}