using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.AttractionCategories
{
    public class DetailsModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

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
    }
}
