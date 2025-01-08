using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Reservations
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.IdAttractionsNavigation)
                .Include(r => r.IdEventsNavigation)
                .Include(r => r.IdUsersNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            Reservation = reservation;

            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Reservation.IdAttractions);
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Reservation.IdEvents);
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Reservation.IdUsers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Reservation.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Reservation.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Reservation.IdEvents);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Reservation.IdUsers);

                return Page();
            }

            if (await _context.Reservations.AnyAsync(r => r.ReservationCode == Reservation.ReservationCode && r.Id != Reservation.Id))
            {
                ModelState.AddModelError("Reservation.ReservationCode", "This Reservation Code already exists. Please provide a unique code.");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Reservation.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Reservation.IdEvents);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Reservation.IdUsers);

                return Page();
            }

            try
            {
                _context.Attach(Reservation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Reservation.Id))
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
