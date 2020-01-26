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

        [HttpPut]
        public IActionResult addPizzaOrder(NewOrderDTO newOrderDto)
        {
            var SINGLE_ORDER_TIME = 30;
            var time = SINGLE_ORDER_TIME;

            var order = new Order();

            order.OrderStatusName = Dto.OrderStatus.IN_REALIZATION.ToString();
            if (newOrderDto.payOnline)
            {
                order.OrderStatusName = Dto.OrderStatus.WAITING_FOR_PAYMENT.ToString();
            }

            order.Phone = newOrderDto.Phone;

            try
            {
                order.Price = getOrderPrice(newOrderDto.PizzaDefinition);
                if(order.Price <= 0)
                {
                    return BadRequest();
                }
            }
            catch (BadRequestException ex)
            {
                return BadRequest();
            }

            _context.Order.ToList().ForEach(it =>
            {
                if (it.OrderStatusName != Dto.OrderStatus.DELIVERED.ToString())
                {
                    time += SINGLE_ORDER_TIME;
                }
            });

            order.PizzaDefinition = JsonConvert.SerializeObject(newOrderDto.PizzaDefinition);
            order.Uid = Guid.NewGuid().ToString();

            //order.Phone = "";
            //order.Uid = "1";
            //order.OrderStatusName = "";
            //order.PizzaDefinition = "";
            //order.Price = 10;

            _context.Order.Add(order);
            _context.SaveChanges();

            return Ok(new NewOrderResponseDTO()
            {
                time = time,
                uid = order.Uid
            }
            );
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
        private IActionResult getOrderPriceFromApi(OrderDefinitionDTO pizzaDefinition)
        {
            try
            {
                return Ok(getOrderPrice(pizzaDefinition));
            } 
            catch (BadRequestException ex)
            {
                return BadRequest();
            }
        }

        private int getOrderPrice(OrderDefinitionDTO pizzaDefinition)
        {
            var DELIVERY_PRICE = 10.0;
            var price = DELIVERY_PRICE;
            double minPizzaPrice = double.MaxValue;

            if (pizzaDefinition.pizza.Count == 0) return 0;

            foreach(var p in pizzaDefinition.pizza)
            {
                foreach(var i in p.Ingredients)
                {
                    var tmpPizzaPrice = 0.0;
                    var ingredient = _context.Ingredient.FirstOrDefault(it => it.Name == i);
                    if(ingredient == null)
                    {
                        throw new BadRequestException();
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
                return (int) price;
            }
            else
            {
                var promotion = _context.Promotion.FirstOrDefault(it => it.Name == pizzaDefinition.PromotionName);
                if(promotion == null)
                {
                    throw new BadRequestException();
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
                return (int) price;
            }

        }

    }

    class BadRequestException : Exception
    {

    }
}