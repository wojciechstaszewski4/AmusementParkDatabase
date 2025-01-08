using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Reservations
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            Reservation = reservation;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                Reservation = reservation;
                _context.Reservations.Remove(Reservation);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
