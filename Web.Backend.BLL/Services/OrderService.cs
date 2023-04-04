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
using Web.Backend.DTO;
using Web.Backend.DTO.CartItem;
using Web.Backend.DTO.Orders;

namespace Web.Backend.BLL.Services
{
    public class OrderService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IPurchaseSessionService purchaseSessionService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public OrderService(SkillkampWdStudyCaseDbContext dbContext,
                            IPurchaseSessionService purchaseSessionService)
        {
            this.dbContext = dbContext;
            this.purchaseSessionService = purchaseSessionService;
        }

        public ServiceResponseModel<OrderDTO> CreateProductOrder(int sessionId)
        {
            var response = new ServiceResponseModel<OrderDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Get session

                    var sessionServiceResponse = purchaseSessionService.CheckActiveSession(sessionId);

                    if(!sessionServiceResponse.IsError && sessionServiceResponse.Item == null)
                    {
                        // Session expire

                        response.ErrorCode = "CP0001";
                        response.ErrorMessage = "Session is expired";

                        return response;
                    }
                    else if (sessionServiceResponse.IsError)
                    {
                        response.ErrorCode = "CP0002";
                        response.ErrorMessage = "Check session error";

                        return response;
                    }

                    // Get product
                }
                catch (Exception)
                {
                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }
    }
}
