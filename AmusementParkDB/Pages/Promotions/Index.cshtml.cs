using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Promotions
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Promotion> Promotion { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Promotion = await _context.Promotions
                .Include(p => p.IdAttractionsNavigation)
                .Include(p => p.IdEventsNavigation)
                .Include(p => p.IdProductsNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Promotions.RemoveRange(_context.Promotions);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
