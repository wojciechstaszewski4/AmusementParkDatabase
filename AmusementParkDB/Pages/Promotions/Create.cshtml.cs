using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Promotions
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Promotion Promotion { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
            ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdEvents"] = new SelectList(_context.Events, "Id", "Id");
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");

                return Page();
            }

            if (Promotion == null)
            {
                return Page();
            }

            _context.Promotions.Add(Promotion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
