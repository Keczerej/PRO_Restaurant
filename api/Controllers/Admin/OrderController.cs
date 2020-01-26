using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Dto;
using Newtonsoft.Json;

namespace api.Controllers.Admin
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private s16271Context _context;

        public OrderController(s16271Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getAllOrders()
        {
            return Ok(_context.Order.ToList().Select(order =>
            {
                var orderDTO = new OrderDTO();
                orderDTO.Uid = order.Uid;
                orderDTO.Phone = order.Phone;
                orderDTO.Price = order.Price;
                orderDTO.PizzaDefinition = JsonConvert.DeserializeObject<OrderDefinitionDTO>(order.PizzaDefinition);
                orderDTO.OrderStatus = (Dto.OrderStatus)Enum.Parse(typeof(Dto.OrderStatus), order.OrderStatusName);
                return orderDTO;
            }));
        }

        [HttpPatch("{uid}/status")]
        public IActionResult changeOrderStatus(String uid, [FromQuery(Name = "newStatus")] Dto.OrderStatus newStatus)
        {
            var order = _context.Order.FirstOrDefault(it => it.Uid == uid);
            if(order == null)
            {
                return BadRequest();
            }
            else
            {
                order.OrderStatusName = newStatus.ToString();
                _context.SaveChanges();
            }
            return Ok();
        }


    }

}