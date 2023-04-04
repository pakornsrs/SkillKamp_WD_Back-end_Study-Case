using AutoMapper;
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
using Web.Backend.DTO.PurchastSession;

namespace Web.Backend.BLL.Services
{
    public class PurchaseSessionService : IPurchaseSessionService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public PurchaseSessionService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<PurchaseSessionDTO> CheckActiveSession(int userId)
        {
            var response = new ServiceResponseModel<PurchaseSessionDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var querry = (from q in this.dbContext.PurchaseSessions
                              where q.UserId == userId && q.Expire > tranDateTime
                              select q).FirstOrDefault();

                if (querry != null)
                {
                    var session = mapper.Map<PurchaseSessionDTO>(querry);
                    response.Item = session;

                    querry.Expire = tranDateTime.AddDays(3);

                    dbContext.Set<PurchaseSession>().Update(querry);
                    dbContext.SaveChanges();
                }

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";

            }
            catch (Exception ex)
            {

                response.Item = null;

                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }

        public ServiceResponseModel<PurchaseSessionDTO> CreateNewSession(int userId)
        {
            var response = new ServiceResponseModel<PurchaseSessionDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var session = new PurchaseSession();

                session.UserId = userId;
                session.CreateDate = tranDateTime;
                session.Expire = tranDateTime.AddDays(3);
                session.UpdateDate = tranDateTime;
                session.TotalPrice = 0;
                session.UpdateDate = tranDateTime;
                session.CreateBy = "system";
                session.UpdateBy = "system";

                dbContext.Set<PurchaseSession>().Add(session);
                dbContext.SaveChanges();

                response.Item = mapper.Map<PurchaseSessionDTO>(session);

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";

            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }

        public ServiceResponseModel<DefaultResponseModel> TerminatePurchastSession(int purchastId)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var query = (from q in this.dbContext.PurchaseSessions
                             where q.Expire > tranDateTime && q.Id == purchastId
                             select q).FirstOrDefault();

                if(query == null)
                {
                    defaultModel.message = "Session not found.";

                    response.Item = defaultModel;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    return response;
                }

                query.Expire = tranDateTime;

                dbContext.Set<PurchaseSession>().Update(query);
                dbContext.SaveChanges();

                defaultModel.message = "Terminate purchast session success.";

                response.Item = defaultModel;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";

            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }

    }
}
