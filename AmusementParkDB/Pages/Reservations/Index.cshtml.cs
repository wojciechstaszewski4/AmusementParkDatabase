using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Reservations
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Reservation> Reservation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.IdAttractionsNavigation)
                .Include(r => r.IdEventsNavigation)
                .Include(r => r.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Reservations.RemoveRange(_context.Reservations);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
