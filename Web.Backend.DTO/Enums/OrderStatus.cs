using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Enums
{
    public enum OrderStatus
    {
        [Description("None")]
        None = 0,

        [Description("InProgress")]
        InProgress = 1,

        [Description("Success")]
        Success = 2,

        [Description("Cancel")]
        Cancel = 3,
    }
}
