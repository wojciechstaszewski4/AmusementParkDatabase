using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.OrderItems
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<OrderItem> OrderItem { get; set; } = default!;

        public async Task OnGetAsync()
        {
            OrderItem = await _context.OrderItems
                .Include(o => o.IdAttractionsNavigation)
                .Include(o => o.IdEventsNavigation)
                .Include(o => o.IdOrdersNavigation)
                .Include(o => o.IdProductsNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.OrderItems.RemoveRange(_context.OrderItems);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
