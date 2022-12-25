using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Model
{
    public class AddressModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Mobile No. is required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string AddressType { get; set; }

        public string Name { get; set; }

    }
}
