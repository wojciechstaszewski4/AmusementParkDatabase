using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Stores
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.IdEmployeesNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (store == null)
            {
                return NotFound();
            }

            Store = store;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.IdEmployeesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
