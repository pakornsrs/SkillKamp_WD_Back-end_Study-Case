using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DTO.Inventories;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.UserTokens;
using Web.Backend.DTO;

namespace Web.Backend.BLL.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public UserTokenService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public ServiceResponseModel<UserTokenDTO> CreateUserToken(int userId, string username, string role)
        //{
        //    var response = new ServiceResponseModel<UserTokenDTO>();
        //    var tranDateTime = DateTimeUtility.GetDateTimeThai();

        //    try
        //    {
        //        var token = JWTUtility.GenerateToken(userId.ToString(), username, role, tranDateTime.AddHours(2));

        //        var userToken = new UserToken();
        //        userToken.Token = token;
        //        userToken.CreateDate = tranDateTime;
        //        userToken.Expire = tranDateTime.AddHours(2);
        //        userToken.UpdateDate = tranDateTime;
        //        userToken.CreateBy = "system";
        //        userToken.UpdateBy = "system";

        //        dbContext.Set<UserToken>().Add(userToken);
        //        dbContext.SaveChanges();

        //        response.Item = mapper.Map<UserTokenDTO>(userToken);

        //        response.ErrorCode = "0000";
        //        response.ErrorMessage = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorCode = "BE9999";
        //        response.ErrorMessage = "Interal server error.";
        //    }

        //    return response;
        //}

        //public ServiceResponseModel<UserTokenDTO> UpdateUserToken(int tokenId)
        //{
        //    var response = new ServiceResponseModel<UserTokenDTO>();
        //    var tranDateTime = DateTimeUtility.GetDateTimeThai();

        //    try
        //    {
        //        var tokenQuery = (from q in this.dbContext.UserTokens
        //                          where q.Id == tokenId
        //                          select q).FirstOrDefault();

        //        tokenQuery.UpdateDate = tranDateTime;
        //        tokenQuery.Expire = tranDateTime.AddHours(2);
        //        tokenQuery.UpdateBy = "system";

        //        dbContext.Set<UserToken>().Update(tokenQuery);
        //        dbContext.SaveChanges();

        //        response.Item = mapper.Map<UserTokenDTO>(tokenQuery);

        //        response.ErrorCode = "0000";
        //        response.ErrorMessage = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorCode = "BE9999";
        //        response.ErrorMessage = "Interal server error.";
        //    }

        //    return response;
        //}
    }
}
