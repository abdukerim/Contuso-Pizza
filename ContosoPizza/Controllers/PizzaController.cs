using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        [HttpGet("{Id}")]
        public ActionResult<Pizza> Get(int Id) 
        {
            var pizza = PizzaService.Get(Id);
            if(pizza == null) 
                return NotFound();
            return pizza;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza) 
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create),new {Id = pizza.Id}, pizza);
        }

        [HttpPut]
        public IActionResult Update(int id, Pizza pizza) 
        {
            if(id != pizza.Id) 
            {
                return BadRequest();
            }
            var existingPizza = PizzaService.Get(id);
            if(existingPizza is null) 
                return NotFound();
            PizzaService.Update(pizza);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);
            if(pizza is null)
                return NotFound();
            PizzaService.Delete(id);
            return NoContent();
        }
    }
}