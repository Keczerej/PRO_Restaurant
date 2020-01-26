using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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