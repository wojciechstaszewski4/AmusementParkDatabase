using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Agreements
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Agreement> Agreement { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Agreement = await _context.Agreements
                .Include(a => a.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Agreements.RemoveRange(_context.Agreements);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
