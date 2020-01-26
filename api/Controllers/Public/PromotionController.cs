using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Public
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
        public IActionResult GetPromotions()
        {
            return Ok(_context.Promotion.ToList());
        }

    }
}