using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using healthPortal.Models;

namespace healthPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly HealthPortalContext _context;

        public RecipesController(HealthPortalContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetRecipes()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Tags) // Include related tags
                .Include(r => r.Ingredients) // Include related ingredients
                .Include(r => r.Instructions) // Include related instructions
                .Include(r => r.Nutritions) // Include related nutrition
                .Select(r => new
                {
                    id = r.Id,
                    title = r.Title,
                    description = r.Description,
                    prep_time = r.PrepTime,
                    image = r.Image,
                    tags = r.Tags.Select(t => t.Name).ToList(), // Extract tag names
                    ingredients = r.Ingredients.Select(i => i.Ingredient1).ToList(), // Use Ingredient1
                    instructions = r.Instructions
                        .OrderBy(instr => instr.StepNumber)
                        .Select(instr => instr.Instruction1) // Use Instruction1
                        .ToList(),
                    nutrition = r.Nutritions.FirstOrDefault() != null ? new
                    {
                        calories = r.Nutritions.FirstOrDefault().Calories,
                        fat = r.Nutritions.FirstOrDefault().Fat,
                        protein = r.Nutritions.FirstOrDefault().Protein,
                        carbs = r.Nutritions.FirstOrDefault().Carbs
                    } : null, // Handle case where Nutritions collection is empty
                    category = r.Category
                })
                .ToListAsync();

            return Ok(recipes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Tags) // Include related tags
                .Include(r => r.Ingredients) // Include related ingredients
                .Include(r => r.Instructions) // Include related instructions
                .Include(r => r.Nutritions) // Include related nutrition
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    id = r.Id,
                    title = r.Title,
                    description = r.Description,
                    prep_time = r.PrepTime,
                    image = r.Image,
                    tags = r.Tags.Select(t => t.Name).ToList(),
                    ingredients = r.Ingredients.Select(i => i.Ingredient1).ToList(),
                    instructions = r.Instructions
                        .OrderBy(instr => instr.StepNumber)
                        .Select(instr => instr.Instruction1)
                        .ToList(),
                    nutrition = r.Nutritions.FirstOrDefault() != null ? new
                    {
                        calories = r.Nutritions.FirstOrDefault().Calories,
                        fat = r.Nutritions.FirstOrDefault().Fat,
                        protein = r.Nutritions.FirstOrDefault().Protein,
                        carbs = r.Nutritions.FirstOrDefault().Carbs
                    } : null,
                    category = r.Category
                })
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }


        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
