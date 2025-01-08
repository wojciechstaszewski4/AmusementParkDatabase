using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Agreements
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Agreement Agreement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .Include(a => a.IdUsersNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (agreement == null)
            {
                return NotFound();
            }

            Agreement = agreement;

            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Agreement.IdUsers);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Agreement.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Agreement.IdUsers);
                return Page();
            }

            try
            {
                _context.Attach(Agreement).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == Agreement.Id))
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
