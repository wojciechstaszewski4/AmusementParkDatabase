using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Stores
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Store Store { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id");
                return Page();
            }

            if (Store == null)
            {
                return Page();
            }

            _context.Stores.Add(Store);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
