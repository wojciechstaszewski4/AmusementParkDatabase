using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Reviews
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Review> Review { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Review = await _context.Reviews
                .Include(r => r.IdAttractionsNavigation)
                .Include(r => r.IdEmployeesNavigation)
                .Include(r => r.IdProductsNavigation)
                .Include(r => r.IdStoresNavigation)
                .Include(r => r.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Reviews.RemoveRange(_context.Reviews);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
