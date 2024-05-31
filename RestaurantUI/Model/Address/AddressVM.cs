using System.ComponentModel.DataAnnotations;

namespace RestaurantUI.Model.Address
{
    public class AddressVM
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal Code must be in format __-___")]
        public string PostalCode { get; set; }
    }
}
