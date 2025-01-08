using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.ACL
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Acl> Acl { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Acl = await _context.Acls
                .Include(a => a.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            _context.Acls.RemoveRange(_context.Acls);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
