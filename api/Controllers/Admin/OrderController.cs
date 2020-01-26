using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;

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

    }
}