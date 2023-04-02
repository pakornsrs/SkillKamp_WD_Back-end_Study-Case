using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ObjectMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Users;

namespace Web.Backend.BLL.Services
{
    public class UserService : IUserService
    {
		private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IRoleService roleService;

        public UserService(SkillkampWdStudyCaseDbContext dbContext,
                           IRoleService roleService)
		{
			this.dbContext = dbContext;
            this.roleService = roleService;
		}
        public async Task<ServiceResponseModel<RegistrationDTO>> Registration(UserDTO userDetail, List<AddressDTO> addressList ,List<CardDTO> userCardList)
        {
            var response = new ServiceResponseModel<RegistrationDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ObjectMapper>());
            var mapper = config.CreateMapper();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
				try
				{
                    // Get user role
                    var role = roleService.GetRoles();

                    // Insert data for table [USER]

                    var user = mapper.Map<User>(userDetail);

                    user.Password = StringHashing.GetHashingString(userDetail.Password);
                    user.BirthDate = DateTimeUtility.ConvertUnixToDateTime(userDetail.BirthDate);

                    user.CreateDate = tranDateTime;
                    user.UpdateDate = tranDateTime;
                    user.CreateBy = "system";
                    user.UpdateBy = "system";
                    user.RoleId = role.Where(item => item.RoleNameEn.ToLower() == "webuser").FirstOrDefault().Id;

                    dbContext.Set<User>().Add(user);
                    dbContext.SaveChanges();

                    // Insert data for table [USER_ADDRESS]

                    var addresses = mapper.Map<List<UserAddress>>(addressList);

                    foreach (var item in addresses)
                    {
                        item.UserId = user.Id;
                        item.CreateDate = tranDateTime;
                        item.UpdateDate = tranDateTime;
                        item.CreateBy = "system";
                        item.UpdateBy = "system";
                    }

                    dbContext.Set<UserAddress>().AddRange(addresses);
                    dbContext.SaveChanges();

                    // Insert data for table [USER_CARD]

                    if(userCardList != null && userCardList.Count > 0)
                    {
                        var cards = mapper.Map<List<UserCard>>(userCardList);

                        foreach (var item in cards)
                        {
                            item.UserId = user.Id;
                            item.CreateDate = tranDateTime;
                            item.UpdateDate = tranDateTime;
                            item.CreateBy = "system";
                            item.UpdateBy = "system";
                        }

                        dbContext.Set<UserCard>().AddRange(cards);
                        dbContext.SaveChanges();
                    }

                    transaction.Commit();

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                }
				catch (Exception ex)
				{
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error";
				}
            }

            return response;
        }
    }
}
