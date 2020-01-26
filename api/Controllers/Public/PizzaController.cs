using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Public
{

    [Route("api/pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        private s16271Context _context;

        public PizzaController(s16271Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getPizza([FromQuery(Name = "integrients")] string[] integrients)
        {
            if(integrients == null || integrients.Length == 0)
            {
                return Ok(_context.PizzaDefinition.ToList());
            }
            else
            {
                return getPizzaByIntegrients(integrients);
            }
        }

        private IActionResult getPizzaByIntegrients(string[] integrients)
        {
            return Ok(_context.PizzaDefinition.Where(
                PizzaDefinition => !integrients.Select(
                    integrient => PizzaDefinition.PizzaIntegrients.FirstOrDefault(it => it.IngredientName == integrient) != null
                ).Contains(false)
            ));;
        }

    }

}