using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL.Entities;
using Web.Backend.DAL;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Enums;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO;

namespace Web.Backend.BLL.Services
{
    public class UserCardService : IUserCardService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public UserCardService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<CardResponseDTO>> GetCardByUserId(int userId)
        {
            var response = new ServiceResponseModel<List<CardResponseDTO>>();

            try
            {
                var query = (from q in this.dbContext.UserCards
                             where q.UserId == userId
                             select q).ToList();

                var cards = mapper.Map<List<CardResponseDTO>>(query);
                var secureKey = GetSecureKeyForUser(userId);

                foreach(var item in cards)
                {
                    item.CardNo = SecureDataUtility.DecryptString(secureKey, item.CardNo);
                    item.CardNo = "xxxx-xxxx-xxxx-" + item.CardNo.Substring(12);
                    item.NameOnCard = SecureDataUtility.DecryptString(secureKey, item.NameOnCard);
                }

                response.Item = cards;

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

        public ServiceResponseModel<ActionResult> AddUserCards(int userId, List<CardRequestDTO> cardList)
        {
            var response = new ServiceResponseModel<ActionResult>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var cards = mapper.Map<List<UserCard>>(cardList);
                var secureKey = GetSecureKeyForUser(userId);

                foreach (var item in cards)
                {
                    var unixTime = cardList.Where(obj => obj.CardNo == item.CardNo).FirstOrDefault();

                    item.UserId = userId;
                    item.CardNo = SecureDataUtility.EncryptString(secureKey, item.CardNo);
                    item.NameOnCard = SecureDataUtility.EncryptString(secureKey, item.NameOnCard);
                    item.CardExpireDate = DateTimeUtility.ConvertUnixToDateTime(unixTime.CardExpireDateUnix.Value);
                    item.Cvv = SecureDataUtility.EncryptString(secureKey, item.Cvv);
                    item.CreateDate = tranDateTime;
                    item.UpdateDate = tranDateTime;
                    item.CreateBy = "system";
                    item.UpdateBy = "system";
                }

                dbContext.Set<UserCard>().AddRange(cards);
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


        #region Private method

        private string GetSecureKeyForUser(int userId)
        {
            try
            {
                var query = (from q in this.dbContext.Users
                             where q.Id == userId
                             select q).FirstOrDefault();

                var userBirthdate = query.BirthDate.Value;
                var userApplyDate = query.CreateDate.Value;

                var key1 = DateTimeUtility.GetUnixTimestamp(userBirthdate.Year, userBirthdate.Month, userBirthdate.Day);
                var key2 = DateTimeUtility.GetUnixTimestamp(userApplyDate.Year, userApplyDate.Month, userApplyDate.Day, userApplyDate.Hour, userApplyDate.Minute, userApplyDate.Second);

                var key = query.Id.ToString() + (key1 + key2).ToString() + "90ba3d3ad15e11edafa10242ac1200023ea707bfd8664e48b27620194512154a";

                return key;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
