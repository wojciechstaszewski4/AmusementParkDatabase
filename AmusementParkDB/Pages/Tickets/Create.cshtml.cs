using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Tickets
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IActionResult OnGet()
        {
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Ticket.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            if (await _context.Tickets.AnyAsync(t => t.TicketNumber == Ticket.TicketNumber))
            {
                ModelState.AddModelError("Ticket.TicketNumber", "This Ticket Number already exists. Please provide a unique number.");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            try
            {
                _context.Tickets.Add(Ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}