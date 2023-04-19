using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.DAL;
using Web.Backend.DTO;
using Web.Backend.DTO.Config;

namespace Web.Backend.BLL.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly SkillkampWdStudyCaseDbContext db;
        public SystemConfigService(SkillkampWdStudyCaseDbContext db)
        {
            this.db = db;
        }
        public ServiceResponseModel<GetConfigDTO<string>> GetVersion()
        {
            var response = new ServiceResponseModel<GetConfigDTO<string>>();

            try
            {
                var result = (from c in this.db.SystemConfigs
                              where c.ConfigName == "API_Version"
                              select new GetConfigDTO<string>
                              {
                                  Data = c.ConfigValue,
                                  DescTh = c.ConfigDescTh,
                                  DescEn = c.ConfigDescEn,
                                  UpdateDate = c.UpdateDate,
                                  UpdateBy = c.UpdateBy
                                  
                              }).FirstOrDefault();

                if(result != null)
                {
                    response.Item = result;
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";
                }
                else
                {
                    response.Item = null;
                    response.ErrorCode = "SF0001";
                    response.ErrorMessage = "Cannot get api version.";
                }

                return response;

            }
            catch (Exception ex)
            {
                response.Item = null;
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Service internal error.";

                return response;
            }
            finally
            {

            }
        }

        public ServiceResponseModel<GetConfigDTO<List<string>>> GetSlideImgPath()
        {
            var response = new ServiceResponseModel<GetConfigDTO<List<string>>>();
            var responseItem = new GetConfigDTO<List<string>>();
            var ItemData = new List<string>();

            try
            {
                var result = (from c in this.db.SystemConfigs
                              where c.ConfigName == "Slide_Path"
                              select new GetConfigDTO<string>
                              {
                                  Data = c.ConfigValue,
                                  DescTh = c.ConfigDescTh,
                                  DescEn = c.ConfigDescEn,
                                  UpdateDate = c.UpdateDate,
                                  UpdateBy = c.UpdateBy

                              }).FirstOrDefault();

                if (result != null)
                {
                    var imgPaths = result.Data.Split(",");

                    foreach(var item in imgPaths)
                    {
                        ItemData.Add(item);
                    }

                    responseItem.Data = ItemData;
                    responseItem.DescTh = result.DescTh;
                    responseItem.DescEn = result.DescEn;
                    responseItem.UpdateDate = result.UpdateDate;
                    responseItem.UpdateBy = result.UpdateBy;


                    response.Item = responseItem;
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";
                }
                else
                {
                    response.Item = null;
                    response.ErrorCode = "SF0001";
                    response.ErrorMessage = "Cannot get api version.";
                }


            }
            catch (Exception ex)
            {
                response.Item = null;
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Service internal error.";

                return response;
            }
            finally
            {

            }

            return response;
        }
    }
}
