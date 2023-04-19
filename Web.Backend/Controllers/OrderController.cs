using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO.ProductReview;
using Web.Backend.Models;
using Web.Backend.Models.Order;
using Web.Backend.Models.Products;

namespace Web.Backend.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IPurchasedOrderService purchasedOrderService;

        public OrderController(IOrderService orderService, IPurchasedOrderService purchasedOrderService)
        {
            this.orderService = orderService;
            this.purchasedOrderService = purchasedOrderService;
        }

        /// <summary>
        /// (To create an order)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     To create order detail that disply on product summary page.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/order/create")]
        public async Task<IActionResult> CreateProductOrder([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<OrderDTO>();

            try
            {
                result = orderService.CreateProductOrder(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        /// <summary>
        /// (To confirm an order)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    To confirm order in summay page and connect to payment process.
        ///    However, payment status is alway mocked as success.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/order/purchase")]
        public async Task<IActionResult> PurchaseOrder([FromBody] PurchaseOrderRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = purchasedOrderService.PurchastOrder(req.userId, req.orderId, req.paymentType, req.cardId, req.addressId, req.addressDetail);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        /// <summary>
        /// (To cancel an order)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    To cancel order, remove all of item in cart and terminate purchast session.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/order/cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] OrderIdRequestModel req)
        {
            var result = new ServiceResponseModel<bool>();

            try
            {
                result = orderService.CancelOrder(req.OrderId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return StatusCode(200, result);
        }

        /// <summary>
        /// (To get order history)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    To get order history displayed in purchast history page.
        ///     
        /// </remarks>
        /// 
        [HttpPost()]
        [Route("api/order/history/detail")]
        public async Task<IActionResult> GetPurchastItemHistory([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<PurchastedProductDTO>>();

            try
            {
                result = purchasedOrderService.GetPurchastItemHistory(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
