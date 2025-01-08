using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.AttractionCategories
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public AttractionCategory AttractionCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attractionCategory = await _context.AttractionCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attractionCategory == null)
            {
                return NotFound();
            }

            AttractionCategory = attractionCategory;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attractionCategoryToDelete = await _context.AttractionCategories.FindAsync(id);

            if (attractionCategoryToDelete != null)
            {
                _context.AttractionCategories.Remove(attractionCategoryToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
