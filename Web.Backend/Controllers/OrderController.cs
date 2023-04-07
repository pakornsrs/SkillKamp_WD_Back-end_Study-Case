using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO;
using Web.Backend.DTO.Orders;
using Web.Backend.Models;
using Web.Backend.Models.Products;

namespace Web.Backend.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost()]
        [Route("api/order/create")]
        public async Task<IActionResult> CreateProductOrder([FromBody] PurchaseSessionIdRequestModel req)
        {
            var result = new ServiceResponseModel<OrderDTO>();

            try
            {
                result = orderService.CreateProductOrder(req.PurchaseSessionId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
