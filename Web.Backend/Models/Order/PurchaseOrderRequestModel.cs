using System.ComponentModel.DataAnnotations;

namespace Web.Backend.Models.Order
{
    public class PurchaseOrderRequestModel
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int orderId { get; set; }

        [Required]
        public int paymentType { get; set; }

        public int cardId { get; set; }
    }
}
