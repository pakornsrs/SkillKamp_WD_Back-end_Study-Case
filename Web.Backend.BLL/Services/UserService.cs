using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO.Users;
using Web.Backend.DTO.UserTokens;

namespace Web.Backend.BLL.Services
{
    public class UserService : IUserService
    {
		private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IRoleService roleService;
        private readonly IUserTokenService userTokenService;
        private readonly IUserAddressService userAddressService;
        private readonly IUserCardService userCardService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public UserService(SkillkampWdStudyCaseDbContext dbContext,
                           IRoleService roleService,
                           IUserTokenService userTokenService,
                           IUserAddressService userAddressService,
                           IUserCardService userCardService)
		{
			this.dbContext = dbContext;
            this.roleService = roleService;
            this.userTokenService = userTokenService;
            this.userAddressService = userAddressService;
            this.userCardService = userCardService;

        }

        #region public method
        public ServiceResponseModel<RegistrationDTO> Registration(UserDTO userDetail, List<AddressDTO> addressList ,List<CardRequestDTO> userCardList)
        {
            var response = new ServiceResponseModel<RegistrationDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
				try
				{
                    // Get user role
                    var roleServiceResponse = roleService.GetRoles();

                    if (roleServiceResponse.IsError)
                    {
                        response.ErrorCode = "RE0001";
                        response.ErrorMessage = "Cannot get user roles";

                        return response;
                    }

                    var role = roleServiceResponse.Item;

                    // Insert data for table [USER]

                    var user = mapper.Map<User>(userDetail);

                    user.Password = HashingUtility.GetHashingString(userDetail.Password);
                    user.BirthDate = DateTimeUtility.ConvertUnixToDateTime(userDetail.BirthDate);

                    user.CreateDate = tranDateTime;
                    user.UpdateDate = tranDateTime;
                    user.CreateBy = "system";
                    user.UpdateBy = "system";
                    user.RoleId = role.Where(item => item.RoleNameEn.ToLower() == "webuser").FirstOrDefault().Id;

                    dbContext.Set<User>().Add(user);
                    dbContext.SaveChanges();

                    // Insert data for table [USER_ADDRESS]

                    var addAddressServiceResponse = userAddressService.AddUserAddresses(user.Id, addressList);

                    if(addAddressServiceResponse.IsError)
                    {
                        response.ErrorCode = "RE0002";
                        response.ErrorMessage = "Insert user address failed.";

                        return response;
                    }

                    var addAddress = addAddressServiceResponse.Item;

                    // Insert data for table [USER_CARD]

                    var addUserCardServiceResponse = userCardService.AddUserCards(user.Id, userCardList);

                    if (addUserCardServiceResponse.IsError)
                    {
                        response.ErrorCode = "RE0003";
                        response.ErrorMessage = "Insert user card failed.";

                        return response;
                    }

                    var addUserCard = addUserCardServiceResponse.Item;

                    // Generate Token and insert to table [User_Token]

                    var userTokenServiceResponse = userTokenService.CreateUserToken(user.Id, user.Username, "webuser");

                    if (userTokenServiceResponse.IsError)
                    {
                        response.ErrorCode = "RE0004";
                        response.ErrorMessage = "Cannot create user token.";

                        return response;
                    }

                    var userToken = userTokenServiceResponse.Item;

                    //Update data for tabel[User]


                   user.UserTokenId = userToken.Id;
                   dbContext.Set<User>().Update(user);
                   dbContext.SaveChanges();

                    response.Item = mapper.Map<RegistrationDTO>(user);
                    response.Item.UserToken = userToken.Token;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();

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

        public ServiceResponseModel<LoginDTO> Login(string username, string password)
        {
            var response = new ServiceResponseModel<LoginDTO>();
            var userDetail = new LoginDTO();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Get user login

                    password = HashingUtility.GetHashingString(password);

                    var userQuery = (from q in this.dbContext.Users
                                 where q.Username == username && q.Password == password
                                 select q).FirstOrDefault();

                    if(userQuery == null)
                    {
                        response.ErrorCode = "LO0001";
                        response.ErrorMessage = "Username or password is incorrect.";

                        return response;
                    }

                    // Update user token
                    var tokenServiceResponse = userTokenService.UpdateUserToken(userQuery.UserTokenId.Value);

                    if (tokenServiceResponse.IsError)
                    {
                        response.ErrorCode = "LO0002";
                        response.ErrorMessage = "User token not found.";

                        return response;
                    }

                    var token = tokenServiceResponse.Item;

                    var user = mapper.Map<UserLoginDTO>(userQuery);

                    userDetail.User = user;
                    userDetail.UserToken = token.Token;

                    response.Item = userDetail;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return response;
        }

        //public ServiceResponseModel<LoginDTO> Logout(int userId)
        //{

        //}

        #endregion
    }
}
