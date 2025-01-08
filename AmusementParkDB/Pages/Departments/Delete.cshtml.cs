using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Departments
{
    public class DeleteModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            Department = department;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Employees)
                .ThenInclude(e => e.Reviews)
                .Include(d => d.Employees)
                .ThenInclude(e => e.Attractions)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

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
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
