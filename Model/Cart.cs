namespace FoodDelivery.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int FoodId { get; set; }

        public int Count { get; set; }
    }
}
