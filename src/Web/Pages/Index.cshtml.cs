using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookCatalogViewModelService _bookcatalogviewmodelservicel;
        private readonly ISubscriptionViewModelService _subscriptionviewmodelservice;

        public IndexModel(ILogger<IndexModel> logger,
                          IBookCatalogViewModelService bookcatalogviewmodelservicel,
                          ISubscriptionViewModelService subscriptionviewmodelservice)
        {
            _logger = logger;
            _bookcatalogviewmodelservicel = bookcatalogviewmodelservicel;
            _subscriptionviewmodelservice = subscriptionviewmodelservice;
        }

        public CatalogueViewModel CatalogViewModel { get; set; } = new CatalogueViewModel();

        public async Task OnGet()
        {
            CatalogViewModel = await _bookcatalogviewmodelservicel.GetBookCatalogueViewModelAsync(User.Identity.Name);
        }

        public async Task OnPostAsync(string BookId)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToPage("/Identity/Account/Login");
            }

            string userid = HttpContext.User.Identity.Name;

            await _subscriptionviewmodelservice.AddSubscriptionAsync(BookId, userid);

            CatalogViewModel = await _bookcatalogviewmodelservicel.GetBookCatalogueViewModelAsync(userid);
        }
    }
}