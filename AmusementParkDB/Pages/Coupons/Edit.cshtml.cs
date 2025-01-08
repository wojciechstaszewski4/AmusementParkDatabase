using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Coupons
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Coupon Coupon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coupon == null)
            {
                return NotFound();
            }

            Coupon = coupon;

            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", Coupon.IdStores);
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Coupon.IdUsers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", Coupon.IdStores);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Coupon.IdUsers);

                return Page();
            }

            if (await _context.Coupons.AnyAsync(c => c.Code == Coupon.Code && c.Id != Coupon.Id))
            {
                ModelState.AddModelError("Coupon.Code", "This Coupon Code already exists. Please provide a unique code.");
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id", Coupon.IdStores);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Coupon.IdUsers);

                return Page();
            }

            try
            {
                _context.Update(Coupon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Coupon.Id))
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
