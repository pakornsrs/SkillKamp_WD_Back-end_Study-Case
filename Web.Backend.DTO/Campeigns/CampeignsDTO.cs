using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Campeigns
{
    public class CampeignsDTO
    {
        public int? Id { get; set; } = null;
        public string? NameTh { get; set; }
        public string? NameEn { get; set; }
        public string? DescTh { get; set; }
        public string? DescEn { get; set; }
        public decimal? DisconutPercent { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
