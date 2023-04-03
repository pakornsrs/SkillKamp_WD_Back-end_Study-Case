using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Enums
{
    public enum ActionResult
    {
        [Description("None")]
        None = 0,

        [Description("Success")]
        Success = 1,

        [Description("Fail")]
        Fail = 2,

        [Description("Exception")]
        Exception = 3,
    }
}
