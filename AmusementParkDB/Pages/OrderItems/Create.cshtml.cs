using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.OrderItems
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IActionResult OnGet()
        {
            ViewData["IdOrders"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("OrderItem.IdOrdersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdOrders"] = new SelectList(_context.Orders, "Id", "Id");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");

                return Page();
            }

            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
