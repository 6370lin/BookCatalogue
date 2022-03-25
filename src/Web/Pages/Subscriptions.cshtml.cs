using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class SubscriptionsModel : PageModel
    {
        private readonly ISubscriptionViewModelService _subscriptionviewmodelservice;
        public SubscriptionsModel(ISubscriptionViewModelService subscriptionviewmodelservice)
        {
            _subscriptionviewmodelservice = subscriptionviewmodelservice;
        }

        public SubscriptionViewModel SubscriptionViewModel { get; set; } = new SubscriptionViewModel();

        public async Task OnGet()
        {
            SubscriptionViewModel =  await _subscriptionviewmodelservice.GetUserSubscriptionViewModelAsync(User.Identity.Name);
        }

        //todo OnGetReadBook() return the text of book so user can read subscribed book
        //todo removeSubscription
    }
}
