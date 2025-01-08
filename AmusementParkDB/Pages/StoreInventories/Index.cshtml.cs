using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.StoreInventories
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<StoreInventory> StoreInventory { get; set; } = default!;

        public async Task OnGetAsync()
        {
            StoreInventory = await _context.StoreInventories
                .Include(s => s.IdProductsNavigation)
                .Include(s => s.IdStoresNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.StoreInventories.RemoveRange(_context.StoreInventories);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
