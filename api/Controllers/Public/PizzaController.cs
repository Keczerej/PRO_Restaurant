using System;
using System.Collections.Generic;
using System.Linq;
using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<PizzaDefinition> pizzaList = null;
            if(integrients == null || integrients.Length == 0)
            {
               pizzaList = _context.PizzaDefinition.Include(it => it.PizzaIntegrients).ToList();
            }
            else
            {
                pizzaList = getPizzaByIntegrients(integrients);
            }
            return Ok(pizzaList.Select(pizza => new PizzaDTO
            {
                Name = pizza.Name,
                Ingredients = pizza.PizzaIntegrients.Select(pi => new IngredientsDTO
                {
                    Name = pi.IngredientName,
                    Price = _context.Ingredient.FirstOrDefault(it => it.Name == pi.IngredientName).Price
                }).ToList()
            }).ToList());;
        }

        private List<PizzaDefinition> getPizzaByIntegrients(string[] integrients)
        {
            return _context.PizzaDefinition.Include(it => it.PizzaIntegrients).Where(
                PizzaDefinition => !integrients.Select(
                    integrient => PizzaDefinition.PizzaIntegrients.FirstOrDefault(it => it.IngredientName == integrient) != null
                ).Contains(false)
            ).ToList();
        }

    }

}