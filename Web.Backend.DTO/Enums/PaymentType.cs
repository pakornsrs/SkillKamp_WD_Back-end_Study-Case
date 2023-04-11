using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Enums
{
    public enum PaymentType
    {
        [Description("None")]
        None = 0,

        [Description("Cash on delivery")]
        OnDelivery = 1,

        [Description("Credit card")]
        CreditCard = 2,

        [Description("Transfer")]
        Transfer = 3,

        [Description("Cash")]
        Cash = 4
    }
}
