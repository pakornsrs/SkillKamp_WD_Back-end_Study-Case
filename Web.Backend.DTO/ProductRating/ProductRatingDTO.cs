using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductRating
{
    public class ProductRatingDTO
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
    }
}
