using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Category
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string? NameTh { get; set; }
        public string? NameEn { get; set; }
        public string? DescTh { get; set; }
        public string? DescEn { get; set; }

    }
}
