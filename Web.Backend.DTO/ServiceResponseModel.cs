using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO
{
    public class ServiceResponseModel<T>
    {
        public T? Item { get; set; }
        public bool IsError => ErrorCode != "0000";
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
