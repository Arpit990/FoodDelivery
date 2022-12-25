namespace FoodDelivery.Model
{
    public class CartRespModel
    {
        public CartRespModel()
        {
            Items = new List<Cart>();
        }
        public List<Cart> Items { get; set; }

        public int TotalItem { get; set; }

        public int TotalPrice { get; set; }
    }
}
