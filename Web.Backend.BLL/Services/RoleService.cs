using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.DAL;
using Web.Backend.DTO;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public RoleService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<RoleDTO>> GetRoles()
        {
            var response = new ServiceResponseModel<List<RoleDTO>>();

            try
            {
                var query = (from q in this.dbContext.Roles
                              select q).ToList();

                var roles = new List<RoleDTO>();

                foreach (var item in query)
                {
                    var role = mapper.Map<RoleDTO>(item);
                    roles.Add(role);
                }

                response.Item = roles;

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
