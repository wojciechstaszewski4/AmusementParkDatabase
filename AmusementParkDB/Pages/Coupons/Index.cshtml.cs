using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Coupons
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Coupon> Coupon { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Coupon = await _context.Coupons
                .Include(c => c.IdStoresNavigation)
                .Include(c => c.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Coupons.RemoveRange(_context.Coupons);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
