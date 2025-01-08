using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Employess
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Employee> Employee { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees
                .Include(e => e.IdDepartmentsNavigation)
                .Include(e => e.IdUsersNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.Attractions)
                .Include(e => e.Reviews)
                .ToListAsync();

            foreach (var employee in employees)
            {
                if (employee.Reviews != null && employee.Reviews.Count > 0)
                {
                    _context.Reviews.RemoveRange(employee.Reviews);
                }

                if (employee.Attractions != null && employee.Attractions.Count > 0)
                {
                    _context.Attractions.RemoveRange(employee.Attractions);
                }
            }

            _context.Employees.RemoveRange(employees);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
