using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SpotleGuessr.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void SendArtist(string artist)
        {

        }
    }
}