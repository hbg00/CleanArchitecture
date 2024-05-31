using Application.DTOs.Address;
using Application.DTOs.Common;

namespace Application.DTOs.Restaurant
{
    public class CreateRestaurantDto : CreateBaseDto
    {
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }

        public AddressDto Address { get; set; }
    }
}