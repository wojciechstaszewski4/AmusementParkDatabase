using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Employess
{
    public class EditModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            Employee = employee;

            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id", Employee.IdDepartments);
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Employee.IdUsers);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id", Employee.IdDepartments);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Employee.IdUsers);

                return Page();
            }

            if (await _context.Employees.AnyAsync(e => e.IdUsers == Employee.IdUsers && e.Id != Employee.Id))
            {
                ModelState.AddModelError("Employee.IdUsers", "The selected User ID is already assigned to another employee. Please choose a different User ID.");
                ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id", Employee.IdDepartments);
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id", Employee.IdUsers);

                return Page();
            }

            try
            {
                _context.Attach(Employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Employees.AnyAsync(e => e.Id == Employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
