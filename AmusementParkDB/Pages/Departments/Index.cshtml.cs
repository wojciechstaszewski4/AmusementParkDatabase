using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmusementParkDB.Pages.Departments
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public IList<Department> Department { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .ThenInclude(e => e.Reviews)
                .Include(d => d.Employees)
                .ThenInclude(e => e.Attractions)
                .ToListAsync();

            foreach (var department in departments)
            {
                foreach (var employee in department.Employees)
                {
                    if (employee.Reviews?.Count > 0)
                    {
                        _context.Reviews.RemoveRange(employee.Reviews);
                    }

                    if (employee.Attractions?.Count > 0)
                    {
                        _context.Attractions.RemoveRange(employee.Attractions);
                    }

                    _context.Employees.Remove(employee);
                }

                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}