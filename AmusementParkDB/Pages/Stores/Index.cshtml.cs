using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Stores
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Store> Store { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Store = await _context.Stores
                .Include(s => s.IdEmployeesNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Stores.RemoveRange(_context.Stores);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
