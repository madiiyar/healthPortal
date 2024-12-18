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
    public class RecipeTagsController : ControllerBase
    {
        private readonly HealthPortalContext _context;

        public RecipeTagsController(HealthPortalContext context)
        {
            _context = context;
        }

        // GET: api/RecipeTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeTag>>> GetRecipeTags()
        {
            return await _context.RecipeTags.ToListAsync();
        }

        // GET: api/RecipeTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeTag>> GetRecipeTag(int id)
        {
            var recipeTag = await _context.RecipeTags.FindAsync(id);

            if (recipeTag == null)
            {
                return NotFound();
            }

            return recipeTag;
        }

        // PUT: api/RecipeTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeTag(int id, RecipeTag recipeTag)
        {
            if (id != recipeTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeTagExists(id))
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

        // POST: api/RecipeTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeTag>> PostRecipeTag(RecipeTag recipeTag)
        {
            _context.RecipeTags.Add(recipeTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeTag", new { id = recipeTag.Id }, recipeTag);
        }

        // DELETE: api/RecipeTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeTag(int id)
        {
            var recipeTag = await _context.RecipeTags.FindAsync(id);
            if (recipeTag == null)
            {
                return NotFound();
            }

            _context.RecipeTags.Remove(recipeTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeTagExists(int id)
        {
            return _context.RecipeTags.Any(e => e.Id == id);
        }
    }
}
