using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Attractions
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Attraction Attraction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["IdAttractionCategories"] = new SelectList(await _context.AttractionCategories.ToListAsync(), "Id", "Id");
            ViewData["IdEmployees"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "Id");
            ViewData["IdEvents"] = new SelectList(await _context.Events.ToListAsync(), "Id", "Id");
            ViewData["IdUsers"] = new SelectList(await _context.Users.ToListAsync(), "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Attraction.IdAttractionCategoriesNavigation");

            if (!ModelState.IsValid)
            {
                ViewData["IdAttractionCategories"] = new SelectList(await _context.AttractionCategories.ToListAsync(), "Id", "Id");
                ViewData["IdEmployees"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "Id");
                ViewData["IdEvents"] = new SelectList(await _context.Events.ToListAsync(), "Id", "Id");
                ViewData["IdUsers"] = new SelectList(await _context.Users.ToListAsync(), "Id", "Id");

                return Page();
            }

            _context.Attractions.Add(Attraction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
