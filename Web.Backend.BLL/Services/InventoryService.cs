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

        public InventoryDTO CreateInventory(InventoryDTO req)
        {
            var response = new InventoryDTO();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var inventory = mapper.Map<ProductInventory>(req);

                inventory.CreateDate = tranDateTime;
                inventory.UpdateDate = tranDateTime;
                inventory.CreateBy = "system";
                inventory.UpdateBy = "system";

                dbContext.Set<ProductInventory>().Add(inventory);
                dbContext.SaveChanges();

                response = mapper.Map<InventoryDTO>(inventory);
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
