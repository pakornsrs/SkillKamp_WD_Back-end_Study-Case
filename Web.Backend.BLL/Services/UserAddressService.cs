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
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public UserAddressService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<AddressDTO>> GetAddressByUserId(int userId)
        {
            var response = new ServiceResponseModel<List<AddressDTO>>();

            try
            {
                var query = (from q in this.dbContext.UserAddresses
                             where q.UserId == userId
                             select q).ToList();

                var addresses = mapper.Map<List<AddressDTO>>(query);
                response.Item = addresses;

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

        public ServiceResponseModel<ActionResult> AddUserAddresses(int userId, List<AddressDTO> addressList)
        {
            var response = new ServiceResponseModel<ActionResult>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var addresses = mapper.Map<List<UserAddress>>(addressList);

                foreach (var item in addresses)
                {
                    item.UserId = userId;
                    item.CreateDate = tranDateTime;
                    item.UpdateDate = tranDateTime;
                    item.CreateBy = "system";
                    item.UpdateBy = "system";
                }

                dbContext.Set<UserAddress>().AddRange(addresses);
                dbContext.SaveChanges();

                response.Item = ActionResult.Success;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.Item = ActionResult.Exception;

                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Interal server error.";
            }

            return response;
        }
    }
}
