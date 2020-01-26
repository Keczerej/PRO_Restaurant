using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Admin
{
    [Route("api/promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {

        private s16271Context _context;

        public PromotionController(s16271Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/promotion/type")]
        public IActionResult getPromotionTypes()
        {
            return Ok(_context.PromotionType.ToList().Select(it => it.Name));
        }

        [HttpPut]
        public IActionResult UpsertPromotion(PromotionDTO promotionDTO)
        {
            var promotion = _context.Promotion.FirstOrDefault(it => it.Name == promotionDTO.Name);
            var isInsert = promotion == null;
            if(isInsert)
            {
                promotion = new Promotion();
            }
            promotion.MinPrice = promotionDTO.MinPrice;
            promotion.Name = promotionDTO.Name;
            promotion.PromotionTypeName = promotionDTO.PromotionType;
            if (isInsert)
            {
                _context.Promotion.Add(promotion);
            }
            else
            {
                _context.Promotion.Update(promotion);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePromotion(string name)
        {
            var promotion = _context.Promotion.FirstOrDefault(it => it.Name == name);
            if(promotion == null)
            {
                return NotFound();
            }
            _context.Promotion.Remove(promotion);
            _context.SaveChanges();
            return Ok();
        }

    }
}