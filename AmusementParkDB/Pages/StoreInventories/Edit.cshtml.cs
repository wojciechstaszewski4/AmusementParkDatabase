using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.StoreInventories
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public StoreInventory StoreInventory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeInventory = await _context.StoreInventories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (storeInventory == null)
            {
                return NotFound();
            }

            StoreInventory = storeInventory;

            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", StoreInventory.IdProducts);
            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", StoreInventory.IdStores);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("StoreInventory.IdStoresNavigation");
            ModelState.Remove("StoreInventory.IdProductsNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", StoreInventory.IdProducts);
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", StoreInventory.IdStores);

                return Page();
            }

            try
            {
                _context.Attach(StoreInventory).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.StoreInventories.AnyAsync(e => e.Id == StoreInventory.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
