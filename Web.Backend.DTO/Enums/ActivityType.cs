using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Enums
{
    public enum ActivityType
    {
        [Description("None")]
        None = 0,

        [Description("New Arrival Product")]
        NewArrival = 1,
    }
}
