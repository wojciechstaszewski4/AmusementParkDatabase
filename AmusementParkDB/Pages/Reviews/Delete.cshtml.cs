using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Reviews
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
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

            var review = await _context.Reviews
                .Include(r => r.IdAttractionsNavigation)
                .Include(r => r.IdEmployeesNavigation)
                .Include(r => r.IdProductsNavigation)
                .Include(r => r.IdStoresNavigation)
                .Include(r => r.IdUsersNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            Review = review;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
