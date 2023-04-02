using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL.Entities;
using Web.Backend.DAL;
using Web.Backend.DTO.Inventories;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.UserTokens;

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

        public UserTokenDTO CreateUserToken(int userId, string username, string role)
        {
            var response = new UserTokenDTO();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var token = JWTUtility.GenerateToken(userId.ToString(), username, role, tranDateTime.AddHours(2));

                var userToken = new UserToken();
                userToken.Token = token;
                userToken.CreateDate = tranDateTime;
                userToken.Expire = tranDateTime.AddHours(2);
                userToken.UpdateDate = tranDateTime;
                userToken.CreateBy = "system";
                userToken.UpdateBy = "system";

                dbContext.Set<UserToken>().Add(userToken);
                dbContext.SaveChanges();

                response = mapper.Map<UserTokenDTO>(userToken);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

        public UserTokenDTO UpdateUserToken(int tokenId)
        {
            var response = new UserTokenDTO();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var tokenQuery = (from q in this.dbContext.UserTokens
                                  where q.Id == tokenId
                                  select q).FirstOrDefault();

                tokenQuery.UpdateDate = tranDateTime;
                tokenQuery.Expire = tranDateTime.AddHours(2);
                tokenQuery.UpdateBy = "system";

                dbContext.Set<UserToken>().Update(tokenQuery);
                dbContext.SaveChanges();

                response = mapper.Map<UserTokenDTO>(tokenQuery);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
