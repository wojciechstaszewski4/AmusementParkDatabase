using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmusementParkDB.Pages
{
    public class IndexModel(ILogger<IndexModel> logger) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;

        public void OnGet()
        {
            _logger.LogInformation("Index page accessed.");
        }
    }
}
