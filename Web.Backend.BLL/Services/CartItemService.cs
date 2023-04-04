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
using Web.Backend.DTO.CartItem;
using Web.Backend.DTO.PurchastSession;

namespace Web.Backend.BLL.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IPurchaseSessionService purchaseSessionService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public CartItemService(SkillkampWdStudyCaseDbContext dbContext, 
                               IPurchaseSessionService purchaseSessionService)
        {
            this.dbContext = dbContext;
            this.purchaseSessionService = purchaseSessionService;
        }

        public ServiceResponseModel<CartItemDTO> AddNewItemToCart(int userId, int productId, int productDetailId, int quantity)
        {
            var response = new ServiceResponseModel<CartItemDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var sessionServiceResponse = purchaseSessionService.CheckActiveSession(userId);
                    var session = new PurchaseSessionDTO();

                    if(!sessionServiceResponse.IsError && sessionServiceResponse.Item == null)
                    {
                        sessionServiceResponse = purchaseSessionService.CreateNewSession(userId);
                    }
                    else if(sessionServiceResponse.IsError)
                    {
                        response.ErrorCode = "AC0001";
                        response.ErrorMessage = "Cannot find or create session.";

                        return response;
                    }

                    session = sessionServiceResponse.Item;

                    var query = (from q in this.dbContext.CartItems
                                 where q.SessionId == session.Id && q.ProductId == productId && q.ProductDetailId == productDetailId
                                 select q).FirstOrDefault();

                    if(query != null)
                    {
                        query.Quantity = query.Quantity + quantity;
                        query.UpdateDate = tranDateTime;
                        query.UpdateBy = "system";

                        dbContext.Set<CartItem>().Update(query);
                        dbContext.SaveChanges();

                        var cartItem = mapper.Map<CartItemDTO>(query);

                        response.Item = cartItem;
                    }
                    else
                    {
                        var item = new CartItem();

                        item.SessionId = session.Id;
                        item.ProductId = productId;
                        item.ProductDetailId = productDetailId;
                        item.Quantity = quantity;
                        item.CreateDate = tranDateTime;
                        item.UpdateDate = tranDateTime;
                        item.CreateBy = "system";
                        item.UpdateBy = "system";

                        dbContext.Set<CartItem>().Add(item);
                        dbContext.SaveChanges();

                        var cartItem = mapper.Map<CartItemDTO>(item);

                        response.Item = cartItem;
                    }

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }

                return response;
            }
        }

        public ServiceResponseModel<CartItemDTO> AddItemQuantityToCart(int cartItemId, int quantity)
        {
            var response = new ServiceResponseModel<CartItemDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var query = (from q in this.dbContext.CartItems
                                 where q.Id == cartItemId
                                 select q).FirstOrDefault();

                    query.Quantity = query.Quantity + quantity;

                    query.UpdateDate = tranDateTime;
                    query.UpdateBy = "system";

                    dbContext.Set<CartItem>().Update(query);
                    dbContext.SaveChanges();

                    var cartItem = mapper.Map<CartItemDTO>(query);

                    response.Item = cartItem;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<CartItemDTO> ReduceItemQuantityInCart(int cartItemId, int quantity)
        {
            var response = new ServiceResponseModel<CartItemDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var query = (from q in this.dbContext.CartItems
                                 where q.Id == cartItemId
                                 select q).FirstOrDefault();

                    if(query == null)
                    {
                        response.ErrorCode = "0000";
                        response.ErrorMessage = "Not found item in cart.";

                        return response;
                    }

                    query.UpdateDate = tranDateTime;
                    query.UpdateBy = "system";

                    if (quantity > 0 && query.Quantity > 0)
                    {
                        query.Quantity = query.Quantity - quantity;

                    }
                    else
                    {
                        response.ErrorCode = "DI0001";
                        response.ErrorMessage = "Cannot decrease quantity.";
                    }

                    var cartItem = mapper.Map<CartItemDTO>(query);

                    if (query.Quantity == 0)
                    {
                        dbContext.Set<CartItem>().Remove(query);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        dbContext.Set<CartItem>().Update(query);
                        dbContext.SaveChanges();
                    }

                    response.Item = cartItem;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<DefaultResponseModel> RemoveItemInCart(int cartItemId)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var query = (from q in this.dbContext.CartItems
                                 where q.Id == cartItemId
                                 select q).FirstOrDefault();

                    dbContext.Set<CartItem>().Remove(query);
                    dbContext.SaveChanges();

                    defaultModel.message = "Romove item success";
                    response.Item = defaultModel;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<DefaultResponseModel> DeleteCartSession(int purchaseSession)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var terminateSessionServiceResponse = purchaseSessionService.TerminatePurchastSession(purchaseSession);

                    if (terminateSessionServiceResponse.IsError)
                    {
                        response.ErrorCode = "DC0001";
                        response.ErrorMessage = "Cannot terminate cart session.";

                        return response;
                    }

                    var query = (from q in this.dbContext.CartItems
                                 where q.SessionId == purchaseSession
                                 select q).ToList();

                    dbContext.Set<CartItem>().RemoveRange(query);
                    dbContext.SaveChanges();

                    defaultModel.message = "Romove item quantity success";
                    response.Item = defaultModel;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }
    }
}
