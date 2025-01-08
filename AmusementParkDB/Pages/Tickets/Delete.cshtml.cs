using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Tickets
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdAttractionsNavigation)
                .Include(t => t.IdEventsNavigation)
                .Include(t => t.IdUsersNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            Ticket = ticket;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdAttractionsNavigation)
                .Include(t => t.IdEventsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            if (ticket.IdAttractionsNavigation != null)
            {
                _context.Attractions.Remove(ticket.IdAttractionsNavigation);
            }

            if (ticket.IdEventsNavigation != null)
            {
                _context.Events.Remove(ticket.IdEventsNavigation);
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
