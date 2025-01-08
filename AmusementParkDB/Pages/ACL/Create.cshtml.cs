using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.ACL
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Acl Acl { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Acl.IdUsersNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                return Page();
            }

            try
            {
                _context.Acls.Add(Acl);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
