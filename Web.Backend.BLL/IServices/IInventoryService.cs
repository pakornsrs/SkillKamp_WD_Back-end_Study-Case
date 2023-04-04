using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.Inventories;

namespace Web.Backend.BLL.IServices
{
    public interface IInventoryService
    {
        public ServiceResponseModel<List<InventoryDTO>> CreateInventory(List<InventoryDTO> req);
    }
}
