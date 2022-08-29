using Microsoft.AspNetCore.Mvc;

namespace AppDev.ViewModels
{
    public class SearchViewModel
    {
        [BindProperty(Name = "Keyword", SupportsGet = true)]
        public string? Keyword { get; set; }
    }
}
