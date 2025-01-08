using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Pages.Coupons
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Coupon Coupon { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

                return Page();
            }

            if (await _context.Coupons.AnyAsync(c => c.Code == Coupon.Code))
            {
                ModelState.AddModelError("Coupon.Code", "This Coupon Code already exists. Please provide a unique code.");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

                return Page();
            }

            _context.Coupons.Add(Coupon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
