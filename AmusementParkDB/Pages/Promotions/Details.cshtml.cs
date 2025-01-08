using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Promotions
{
    public class DetailsModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (promotion == null)
            {
                return NotFound();
            }

            Promotion = promotion;

            return Page();
        }
    }
}
