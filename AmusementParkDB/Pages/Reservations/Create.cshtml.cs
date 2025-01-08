using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Pages.Reservations
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IActionResult OnGet()
        {
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Reservation.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");

                return Page();
            }

            if (await _context.Reservations.AnyAsync(r => r.ReservationCode == Reservation.ReservationCode))
            {
                ModelState.AddModelError("Reservation.ReservationCode", "This Reservation Code already exists. Please provide a unique code.");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");

                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
