using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Agreements
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Agreement Agreement { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Agreement.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                return Page();
            }

            _context.Agreements.Add(Agreement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
