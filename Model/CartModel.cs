using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Model
{
    public class CartModel
    {

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FoodId is required")]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "Count is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Count Should be Greater than 0.")]
        public int Count { get; set; }
    }
}
