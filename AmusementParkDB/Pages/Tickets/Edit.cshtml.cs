using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Tickets
{
    public class EditModel(AmusementParkDbContext context) : PageModel
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            Ticket = ticket;

            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Ticket.IdAttractions);
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Ticket.IdEvents);
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Ticket.IdUsers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Ticket.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Ticket.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Ticket.IdEvents);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Ticket.IdUsers);
                return Page();
            }

            if (await _context.Tickets.AnyAsync(t => t.TicketNumber == Ticket.TicketNumber && t.Id != Ticket.Id))
            {
                ModelState.AddModelError("Ticket.TicketNumber", "This Ticket Number already exists. Please provide a unique number.");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id", Ticket.IdAttractions);
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id", Ticket.IdEvents);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Ticket.IdUsers);

                return Page();
            }

            try
            {
                _context.Attach(Ticket).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Ticket.Id))
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