using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.StoreInventories
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public StoreInventory StoreInventory { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("StoreInventory.IdStoresNavigation");
            ModelState.Remove("StoreInventory.IdProductsNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

                return Page();
            }

            if (StoreInventory == null)
            {
                return Page();
            }

            _context.StoreInventories.Add(StoreInventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
