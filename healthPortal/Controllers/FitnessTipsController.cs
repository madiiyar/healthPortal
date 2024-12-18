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
    public class FitnessTipsController : ControllerBase
    {
        private readonly HealthPortalContext _context;

        public FitnessTipsController(HealthPortalContext context)
        {
            _context = context;
        }

        // GET: api/FitnessTips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFitnessTips()
        {
            var fitnessTips = await _context.FitnessTips
                .Include(ft => ft.Tags) // Include related tags
                .ToListAsync(); // Fetch raw data first

            // Transform data manually after fetching it
            var formattedFitnessTips = fitnessTips.Select(ft => new
            {
                id = ft.Id,
                title = ft.Title,
                image = ft.Image,
                fitnessLevel = ft.FitnessLevel,
                tags = ft.Tags.Select(t => t.Name).ToList(),
                views = ft.Views,
                date = $"{ft.Date:MMM dd, yyyy}" // Manual date formatting
            });

            return Ok(formattedFitnessTips);
        }




        // GET: api/FitnessTips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFitnessTip(int id)
        {
            var fitnessTip = await _context.FitnessTips
                .Include(ft => ft.Tags) // Include related tags
                .FirstOrDefaultAsync(ft => ft.Id == id); // Fetch the specific FitnessTip

            if (fitnessTip == null)
            {
                return NotFound();
            }

            // Transform the data to the required format
            var result = new
            {
                id = fitnessTip.Id,
                title = fitnessTip.Title,
                image = fitnessTip.Image,
                fitnessLevel = fitnessTip.FitnessLevel,
                tags = fitnessTip.Tags.Select(t => t.Name).ToList(), // Extract tag names
                views = fitnessTip.Views,
                date = $"{fitnessTip.Date:MMM dd, yyyy}" // Manual date formatting
            };

            return Ok(result);
        }


        // PUT: api/FitnessTips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFitnessTip(int id, FitnessTip fitnessTip)
        {
            if (id != fitnessTip.Id)
            {
                return BadRequest();
            }

            _context.Entry(fitnessTip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FitnessTipExists(id))
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

        // POST: api/FitnessTips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FitnessTip>> PostFitnessTip(FitnessTip fitnessTip)
        {
            _context.FitnessTips.Add(fitnessTip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFitnessTip", new { id = fitnessTip.Id }, fitnessTip);
        }

        // DELETE: api/FitnessTips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFitnessTip(int id)
        {
            var fitnessTip = await _context.FitnessTips.FindAsync(id);
            if (fitnessTip == null)
            {
                return NotFound();
            }

            _context.FitnessTips.Remove(fitnessTip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FitnessTipExists(int id)
        {
            return _context.FitnessTips.Any(e => e.Id == id);
        }
    }
}
