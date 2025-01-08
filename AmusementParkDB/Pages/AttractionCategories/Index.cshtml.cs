using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.AttractionCategories
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<AttractionCategory> AttractionCategory { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AttractionCategory = await _context.AttractionCategories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.AttractionCategories.RemoveRange(_context.AttractionCategories);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
