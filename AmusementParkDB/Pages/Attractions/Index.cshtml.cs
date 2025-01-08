using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Attractions
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Attraction> Attraction { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Attraction = await _context.Attractions
                .Include(a => a.IdAttractionCategoriesNavigation)
                .Include(a => a.IdEmployeesNavigation)
                .Include(a => a.IdEventsNavigation)
                .Include(a => a.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Attractions.RemoveRange(_context.Attractions);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
