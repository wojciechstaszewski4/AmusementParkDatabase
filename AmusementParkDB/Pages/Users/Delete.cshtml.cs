using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Users
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public new User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Reviews)
                .Include(u => u.Reservations)
                .Include(u => u.Tickets)
                .Include(u => u.Acls)
                .Include(u => u.Agreements)
                .Include(u => u.Coupons)
                .Include(u => u.Orders)
                .Include(u => u.Attractions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user != null)
            {
                if (user.Reviews?.Count > 0) _context.Reviews.RemoveRange(user.Reviews);
                if (user.Reservations?.Count > 0) _context.Reservations.RemoveRange(user.Reservations);
                if (user.Tickets?.Count > 0) _context.Tickets.RemoveRange(user.Tickets);
                if (user.Acls?.Count > 0) _context.Acls.RemoveRange(user.Acls);
                if (user.Agreements?.Count > 0) _context.Agreements.RemoveRange(user.Agreements);
                if (user.Coupons?.Count > 0) _context.Coupons.RemoveRange(user.Coupons);
                if (user.Orders?.Count > 0) _context.Orders.RemoveRange(user.Orders);
                if (user.Attractions?.Count > 0) _context.Attractions.RemoveRange(user.Attractions);

                var employees = await _context.Employees
                    .Where(e => e.IdUsers == user.Id)
                    .Include(e => e.Reviews)
                    .ToListAsync();

                foreach (var employee in employees)
                {
                    if (employee.Reviews?.Count > 0)
                    {
                        _context.Reviews.RemoveRange(employee.Reviews);
                    }

                    _context.Employees.Remove(employee);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}