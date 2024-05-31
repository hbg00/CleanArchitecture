using RestaurantUI.Model.Address;
using System.ComponentModel.DataAnnotations;

namespace RestaurantUI.Model.Restaurant
{
    public class CreateRestaurantVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }

        [Required]
        public bool HasDelivery { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        public string ContactName { get; set; }

        public AddressVM Address { get; set; }
    }
}
