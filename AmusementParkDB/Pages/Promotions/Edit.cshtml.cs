using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Promotions
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Promotion Promotion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .Include(p => p.IdAttractionsNavigation)
                .Include(p => p.IdEventsNavigation)
                .Include(p => p.IdProductsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (promotion == null)
            {
                return NotFound();
            }

            Promotion = promotion;

            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Promotion.IdAttractions);
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Promotion.IdEvents);
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", Promotion.IdProducts);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Promotion.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Promotion.IdEvents);
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", Promotion.IdProducts);

                return Page();
            }

            try
            {
                _context.Update(Promotion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Stores.AnyAsync(e => e.Id == Promotion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
