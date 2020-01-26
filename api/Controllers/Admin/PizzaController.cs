using System;
using System.Linq;
using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers.Admin
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

        [HttpPut]
        public IActionResult UpsertPizza(PizzaDTO pizzaDTO)
        {
            var pizza = _context.PizzaDefinition.Include(it => it.PizzaIntegrients).FirstOrDefault(it => it.Name == pizzaDTO.Name);
            var isInsert = pizza == null;
            if (isInsert)
            {
                pizza = new PizzaDefinition();
            }
            pizza.Name = pizzaDTO.Name;
            pizza.PizzaIntegrients.ToList().ForEach(it => _context.PizzaIntegrients.Remove(it));
            pizza.PizzaIntegrients = pizzaDTO.Ingredients.Select(ingredientDTO =>
            {
                var ingredient = _context.Ingredient.FirstOrDefault(it => it.Name == ingredientDTO.Name);
                var isIngredientInsert = ingredient == null;
                if (isIngredientInsert)
                {
                    ingredient = new Ingredient();
                }
                ingredient.Name = ingredientDTO.Name;
                ingredient.Price = ingredientDTO.Price;
                if (isIngredientInsert)
                {
                    _context.Ingredient.Add(ingredient);
                }
                else
                {
                    _context.Ingredient.Update(ingredient);
                }
                var pizzaIngredient = new PizzaIntegrients();
                pizzaIngredient.IngredientName = ingredient.Name;
                pizzaIngredient.PizzaName = pizza.Name;
                return pizzaIngredient;
            }).ToList();
            if (isInsert)
            {
                _context.PizzaDefinition.Add(pizza);
            }
            else
            {
                _context.PizzaDefinition.Update(pizza);
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
        public IActionResult DeletePizza(string name)
        {
            var pizza = _context.PizzaDefinition.FirstOrDefault(it => it.Name == name);
            if (pizza == null)
            {
                return NotFound();
            }
            _context.PizzaDefinition.Remove(pizza);
            _context.SaveChanges();
            return Ok();
        }


    }

}