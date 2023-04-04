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
using Web.Backend.DTO.Campeigns;
using Web.Backend.DTO.Users;

namespace Web.Backend.BLL.Services
{
    public class DiscountCampeignService : IDiscountCampeignService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public DiscountCampeignService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<CampeignsDTO> AddDiscountCampeign(CampeignsDTO campeignReq)
        {
            var response = new ServiceResponseModel<CampeignsDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var campeign = mapper.Map<DiscountCampeign>(campeignReq);
                    
                    campeign.CreateDate = tranDateTime;
                    campeign.UpdateDate = tranDateTime;
                    campeign.CreateBy = "system"; // TO DO : use admin username from header
                    campeign.UpdateBy = "system"; // TO DO : use admin username from header

                    dbContext.Set<DiscountCampeign>().Add(campeign);
                    dbContext.SaveChanges();

                    response.Item = mapper.Map<CampeignsDTO>(campeign);
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<CampeignsDTO> UpdateDiscountCampeign(CampeignsDTO campeignReq)
        {
            var response = new ServiceResponseModel<CampeignsDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var campeign = mapper.Map<DiscountCampeign>(campeignReq);

                    campeign.UpdateDate = tranDateTime;
                    campeign.UpdateBy = "system"; // TO DO : use admin username from header

                    dbContext.Set<DiscountCampeign>().Update(campeign);
                    dbContext.SaveChanges();

                    response.Item = mapper.Map<CampeignsDTO>(campeign);
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<string> TerminateDiscountCampeign(List<int> campeignIdList)
        {
            var response = new ServiceResponseModel<string>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var query = (from q in this.dbContext.DiscountCampeigns
                                 where campeignIdList.Contains(q.Id)
                                 select q).ToList();

                    foreach(var item in query)
                    {
                        item.IsActive = false;
                        item.EndDate = tranDateTime;
                        item.UpdateDate = tranDateTime;
                        item.UpdateBy = "system"; // TO DO : use admin username from header
                    }

                    dbContext.Set<DiscountCampeign>().UpdateRange(query);
                    dbContext.SaveChanges();

                    response.Item = "Terminate success";

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }

            }

            return response;
        }
    }
}
