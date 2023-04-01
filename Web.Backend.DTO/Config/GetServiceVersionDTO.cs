using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Config
{
    public class GetServiceVersionDTO
    {
        public string Version { get; set; }
        public string DescTh { get; set; }
        public string DescEn { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
