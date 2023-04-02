using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.DAL;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        public RoleService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<RoleDTO> GetRoles()
        {
            var response = new List<RoleDTO>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>());
            var mapper = config.CreateMapper();

            try
            {
                var query = (from q in this.dbContext.Roles
                              select q).ToList();

                foreach(var item in query)
                {
                    var role = mapper.Map<RoleDTO>(item);
                    response.Add(role);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
