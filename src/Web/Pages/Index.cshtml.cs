using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IBookCatalogViewModelService _bookcatalogueviewmodelservicel;

        public IndexModel(ILogger<IndexModel> logger,
                          IBookCatalogViewModelService bookcatalogueviewmodelservicel)
        {
            _logger = logger;
            _bookcatalogueviewmodelservicel = bookcatalogueviewmodelservicel;
        }

        public CatalogueViewModel CatalogViewModel { get; set; } = new CatalogueViewModel();

        public async Task OnGet()
        {
            CatalogViewModel = await _bookcatalogueviewmodelservicel.GetBookCatalogueViewModelAsync();
        }
    }
}