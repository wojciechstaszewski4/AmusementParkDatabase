using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Reviews
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Review Review { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            Review = review;

            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Review.IdUsers);
            ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id", Review.IdEmployees);
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Review.IdAttractions);
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", Review.IdProducts);
            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", Review.IdStores);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Review.IdUsers);
                ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id", Review.IdEmployees);
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Review.IdAttractions);
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", Review.IdProducts);
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", Review.IdStores);

                return Page();
            }

            try
            {
                _context.Attach(Review).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Review.Id))
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
