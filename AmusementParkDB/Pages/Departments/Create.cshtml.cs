using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AmusementParkDB.Data;
using AmusementParkDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Pages.Departments
{
    public class CreateModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        [BindProperty]
        public Department Department { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _context.Departments.AnyAsync(d => d.Name == Department.Name))
            {
                ModelState.AddModelError("Department.Name", "A department with this name already exists. Please provide a unique name.");
                return Page();
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
