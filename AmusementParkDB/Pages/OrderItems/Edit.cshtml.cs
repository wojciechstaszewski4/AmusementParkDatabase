using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.OrderItems
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.IdOrdersNavigation)
                .Include(o => o.IdAttractionsNavigation)
                .Include(o => o.IdEventsNavigation)
                .Include(o => o.IdProductsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            OrderItem = orderItem;

            ViewData["IdOrders"] = new SelectList(_context.Orders, "Id", "Id", OrderItem.IdOrders);
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", OrderItem.IdAttractions);
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", OrderItem.IdEvents);
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", OrderItem.IdProducts);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("OrderItem.IdOrdersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdOrders"] = new SelectList(_context.Orders, "Id", "Id", OrderItem.IdOrders);
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", OrderItem.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", OrderItem.IdEvents);
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id", OrderItem.IdProducts);

                return Page();
            }

            try
            {
                _context.Update(OrderItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == OrderItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
