using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmusementParkDB.Data;
using AmusementParkDB.Models;

namespace AmusementParkDB.Pages.Users
{
    public class IndexModel(AmusementParkDbContext context) : PageModel
    {
        private readonly AmusementParkDbContext _context = context;

        public new IList<User> User { get; set; } = default!;
        private static readonly Dictionary<int, string> _originalNames = [];
        private static bool _isAnonymized = false;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "Id";

        public async Task OnGetAsync()
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(u => u.Surname.Contains(SearchTerm));
            }

            query = SortOrder switch
            {
                "Id" => query.OrderBy(u => u.Id),
                "Surname" => query.OrderBy(u => u.Surname),
                _ => query.OrderBy(u => u.Name),
            };

            User = await query.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.Reviews)
                .Include(u => u.Reservations)
                .Include(u => u.Tickets)
                .Include(u => u.Acls)
                .Include(u => u.Agreements)
                .Include(u => u.Coupons)
                .Include(u => u.Orders)
                .Include(u => u.Attractions)
                .ToListAsync();

            foreach (var user in users)
            {
                if (user.Reviews?.Count > 0) _context.Reviews.RemoveRange(user.Reviews);
                if (user.Reservations?.Count > 0) _context.Reservations.RemoveRange(user.Reservations);
                if (user.Tickets?.Count > 0) _context.Tickets.RemoveRange(user.Tickets);
                if (user.Acls?.Count > 0) _context.Acls.RemoveRange(user.Acls);
                if (user.Agreements?.Count > 0) _context.Agreements.RemoveRange(user.Agreements);
                if (user.Coupons?.Count > 0) _context.Coupons.RemoveRange(user.Coupons);
                if (user.Orders?.Count > 0) _context.Orders.RemoveRange(user.Orders);

                var employees = await _context.Employees
                    .Where(e => e.IdUsers == user.Id)
                    .Include(e => e.Reviews)
                    .Include(e => e.Attractions)
                    .ToListAsync();

                foreach (var employee in employees)
                {
                    if (employee.Attractions?.Count > 0)
                    {
                        _context.Attractions.RemoveRange(employee.Attractions);
                    }

                    if (employee.Reviews?.Count > 0)
                    {
                        _context.Reviews.RemoveRange(employee.Reviews);
                    }

                    _context.Employees.Remove(employee);
                }

                if (user.Attractions?.Count > 0) _context.Attractions.RemoveRange(user.Attractions);
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAnonymizeAsync()
        {
            var users = await _context.Users.ToListAsync();

            if (_isAnonymized)
            {
                foreach (var user in users)
                {
                    if (_originalNames.TryGetValue(user.Id, out var originalName))
                    {
                        user.Name = originalName;
                    }
                }

                _isAnonymized = false;
            }
            else
            {
                foreach (var user in users)
                {
                    if (!_originalNames.ContainsKey(user.Id))
                    {
                        _originalNames[user.Id] = user.Name;
                    }

                    user.Name = $"User-{user.Id}";
                }

                _isAnonymized = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}