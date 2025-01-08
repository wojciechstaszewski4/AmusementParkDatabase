using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Orders
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Order> Orders { get; set; } = default!;
        public IList<OrderItem> OrderItems { get; set; } = default!;
        public int? SelectedOrderId { get; set; }

        public async Task OnGetAsync(int? orderId)
        {
            Orders = await _context.Orders
                .Include(o => o.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();

            if (orderId.HasValue)
            {
                SelectedOrderId = orderId;
                OrderItems = await _context.OrderItems
                    .Where(oi => oi.IdOrders == orderId.Value)
                    .Include(oi => oi.IdAttractionsNavigation)
                    .Include(oi => oi.IdEventsNavigation)
                    .Include(oi => oi.IdProductsNavigation)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            foreach (var order in orders)
            {
                if (order.OrderItems?.Count > 0)
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                }

                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
