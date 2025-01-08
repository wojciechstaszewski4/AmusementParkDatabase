using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Attractions
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Attraction Attraction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attraction = await _context.Attractions.FindAsync(id);

            if (attraction == null)
            {
                return NotFound();
            }

            Attraction = attraction;

            ViewData["IdAttractionCategories"] = new SelectList(await _context.AttractionCategories.ToListAsync(), "Id", "Id", Attraction.IdAttractionCategories);
            ViewData["IdEmployees"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "Id", Attraction.IdEmployees);
            ViewData["IdEvents"] = new SelectList(await _context.Events.ToListAsync(), "Id", "Id", Attraction.IdEvents);
            ViewData["IdUsers"] = new SelectList(await _context.Users.ToListAsync(), "Id", "Id", Attraction.IdUsers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Attraction.IdAttractionCategoriesNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdAttractionCategories"] = new SelectList(await _context.AttractionCategories.ToListAsync(), "Id", "Id", Attraction.IdAttractionCategories);
                ViewData["IdEmployees"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "Id", Attraction.IdEmployees);
                ViewData["IdEvents"] = new SelectList(await _context.Events.ToListAsync(), "Id", "Id", Attraction.IdEvents);
                ViewData["IdUsers"] = new SelectList(await _context.Users.ToListAsync(), "Id", "Id", Attraction.IdUsers);

                return Page();
            }

            try
            {
                _context.Attach(Attraction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Attraction.Id))
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
