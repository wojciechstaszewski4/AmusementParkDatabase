using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Reviews
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Review Review { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
            ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["IdEmployees"] = new SelectList(_context.Employees, "Id", "Id");
                ViewData["IdAttractions"] = new SelectList(_context.Attractions, "Id", "Id");
                ViewData["IdProducts"] = new SelectList(_context.Products, "Id", "Id");
                ViewData["IdStores"] = new SelectList(_context.Stores, "Id", "Id");

                return Page();
            }

            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
