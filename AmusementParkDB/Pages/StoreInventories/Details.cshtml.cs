using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.StoreInventories
{
    public class DetailsModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public StoreInventory StoreInventory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeInventory = await _context.StoreInventories
                .Include(s => s.IdProductsNavigation)
                .Include(s => s.IdStoresNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (storeInventory == null)
            {
                return NotFound();
            }

            StoreInventory = storeInventory;

            return Page();
        }
    }
}
