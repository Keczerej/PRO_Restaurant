using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Controllers.Public
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


        [HttpGet("{uid}")]
        public IActionResult getOrderByUid(String uid)
        {
            var order = _context.Order.FirstOrDefault(it => it.Uid == uid);
            if(order == null)
            {
                return NotFound();
            }
            else
            {
                var orderDTO = new OrderDTO();
                orderDTO.Uid = order.Uid;
                orderDTO.Phone = order.Phone;
                orderDTO.Price = order.Price;
                orderDTO.PizzaDefinition = JsonConvert.DeserializeObject<OrderDefinitionDTO>(order.PizzaDefinition);
                orderDTO.OrderStatus = (Dto.OrderStatus) Enum.Parse(typeof(Dto.OrderStatus), order.OrderStatusName);
                return Ok(orderDTO);
            }
        }

        [HttpPost("{uid}/paid")]
        public IActionResult paidOrderByUid(String uid)
        {
            var order = _context.Order.FirstOrDefault(it => it.Uid == uid);
            if (order == null)
            {
                return NotFound();
            }
            if(order.OrderStatusName == Dto.OrderStatus.WAITING_FOR_PAYMENT.ToString())
            {
                order.OrderStatusName = Dto.OrderStatus.IN_REALIZATION.ToString();
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{uid}/price")]
        public IActionResult getOrderPrice(OrderDefinitionDTO pizzaDefinition)
        {
            var DELIVERY_PRICE = 10.0;
            var price = DELIVERY_PRICE;
            double minPizzaPrice = double.MaxValue;

            if (pizzaDefinition.pizza.Count == 0) return Ok(0.0);

            foreach(var p in pizzaDefinition.pizza)
            {
                foreach(var i in p.Ingredients)
                {
                    var tmpPizzaPrice = 0.0;
                    var ingredient = _context.Ingredient.FirstOrDefault(it => it.Name == i);
                    if(ingredient == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        price += ingredient.Price;
                        tmpPizzaPrice += ingredient.Price;
                    }
                    if(tmpPizzaPrice < minPizzaPrice)
                    {
                        minPizzaPrice = tmpPizzaPrice;
                    }
                };
            };

            if(pizzaDefinition.PromotionName == null)
            {
                return Ok(price);
            }
            else
            {
                var promotion = _context.Promotion.FirstOrDefault(it => it.Name == pizzaDefinition.PromotionName);
                if(promotion == null)
                {
                    return BadRequest();
                }
                else
                {
                    Dto.PromotionType type = (Dto.PromotionType)Enum.Parse(typeof(Dto.PromotionType), promotion.PromotionTypeName);
                    switch (type)
                    {
                        case Dto.PromotionType.FREE_DELIVERY:
                            {
                                if(price >= promotion.MinPrice)
                                {
                                    price -= DELIVERY_PRICE;
                                }
                                break;
                            }
                        case Dto.PromotionType.THIRD_PIZZA_GRATIS:
                            {
                                if (price >= promotion.MinPrice && pizzaDefinition.pizza.Count > 2)
                                {
                                    price -= minPizzaPrice; 
                                }
                                break;
                            }
                        case Dto.PromotionType.THIRTY_PERCENT_OFF:
                            {
                                if (price >= promotion.MinPrice)
                                {
                                    price *= 0.7;
                                }
                                break;
                            }
                    }
                }
                return Ok(price);
            }

        }

    }


}