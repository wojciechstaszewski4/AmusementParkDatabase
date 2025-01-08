using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Tickets
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Ticket> Ticket { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Ticket = await _context.Tickets
                .Include(t => t.IdAttractionsNavigation)
                .Include(t => t.IdEventsNavigation)
                .Include(t => t.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Tickets.RemoveRange(_context.Tickets);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
