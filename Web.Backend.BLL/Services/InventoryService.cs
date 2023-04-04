using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.Users;

namespace Web.Backend.BLL.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public InventoryService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<InventoryDTO>> CreateInventory(List<InventoryDTO> req)
        {
            var response = new ServiceResponseModel<List<InventoryDTO>>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var inventoryies = new List<ProductInventory>();
                
                foreach(var item in req)
                {
                    var inventory = mapper.Map<ProductInventory>(item);

                    inventory.CreateDate = tranDateTime;
                    inventory.UpdateDate = tranDateTime;
                    inventory.CreateBy = "system";
                    inventory.UpdateBy = "system";

                    inventoryies.Add(inventory);
                }

                dbContext.Set<ProductInventory>().AddRange(inventoryies);
                dbContext.SaveChanges();

                var result = new List<InventoryDTO>();

                foreach(var item in inventoryies)
                {
                    var _result = mapper.Map<InventoryDTO>(item);
                    result.Add(_result);
                }

                response.Item = result;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Interal server error.";
            }

            return response;
        }
    }
}
