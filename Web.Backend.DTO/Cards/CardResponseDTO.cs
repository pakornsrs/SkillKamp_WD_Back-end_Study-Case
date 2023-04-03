﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Cards
{
    public class CardResponseDTO
    {
        public int Id { get;set; }
        public int UserId { get; set; }
        public string? CardNo { get; set; }
        public string? NameOnCard { get; set; }
        public int? Provider { get; set; }
        public DateTime? CardExpireDate { get; set; }
    }
}
