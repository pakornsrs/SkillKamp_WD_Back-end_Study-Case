using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Enums
{
    public enum Genders
    {
        [Description("None")]
        None = 0,

        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,

        [Description("Others")]
        Others = 3
    }
}
