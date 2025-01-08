using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Users
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public new User User { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _context.Users.AnyAsync(u => u.Email == User.Email))
            {
                ModelState.AddModelError("User.Email", "The email address is already in use. Please use a different email.");
                return Page();
            }

            if (await _context.Users.AnyAsync(u => u.Login == User.Login))
            {
                ModelState.AddModelError("User.Login", "The login is already in use. Please choose a different login.");
                return Page();
            }

            try
            {
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving. Please try again.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}