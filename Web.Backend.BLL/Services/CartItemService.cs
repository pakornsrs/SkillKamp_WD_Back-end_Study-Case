using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.DAL;

namespace Web.Backend.BLL.Services
{
    internal class CartItemService : ICartItemService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public CartItemService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
