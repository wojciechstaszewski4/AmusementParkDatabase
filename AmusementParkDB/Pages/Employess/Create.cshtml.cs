using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Employess
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            if (await _context.Employees.AnyAsync(e => e.IdUsers == Employee.IdUsers))
            {
                ModelState.AddModelError("Employee.IdUsers", "The selected User ID is already assigned to another employee. Please choose a different User ID.");
                ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            try
            {
                _context.Employees.Add(Employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Id");
                ViewData["IdUsers"] = new SelectList(_context.Users, "Id", "Id");

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
