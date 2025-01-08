using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Users
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public new User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (User == null)
            {
                return NotFound();
            }

            if (await _context.Users.AnyAsync(u => u.Email == User.Email && u.Id != User.Id))
            {
                ModelState.AddModelError("User.Email", "The email address is already in use. Please use a different email.");
                return Page();
            }

            if (await _context.Users.AnyAsync(u => u.Login == User.Login && u.Id != User.Id))
            {
                ModelState.AddModelError("User.Login", "The login is already in use. Please choose a different login.");
                return Page();
            }

            try
            {
                _context.Attach(User).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tickets.AnyAsync(e => e.Id == User.Id))
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