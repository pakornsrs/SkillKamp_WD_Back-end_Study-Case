using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductColors
{
    public class ProductColorDTO
    {
        public int Id { get; set; }
        public string? ColorNameTh { get; set; }
        public string? ColorNameEn { get; set; }
        public string? ColorCode { get; set; }
        public string? ColorCodeRgb { get; set; }
    }
}
