namespace Web.Backend.Models.CartItems
{
    public class AddItmeRequestModel
    {
        public int userId { get; set; }
        public int productId { get; set; }
        public int productDetail { get; set; }
        public int quantity { get; set; }
    }
}
