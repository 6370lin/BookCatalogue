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

        public string BookText = string.Empty;

        public async Task OnGet()
        {
            SubscriptionViewModel =  await _subscriptionviewmodelservice.GetUserSubscriptionViewModelAsync(User.Identity.Name);
        }

        public async Task OnPostReadBookAsync(Guid subscriptionid)
        {
            BookText = await _subscriptionviewmodelservice.GetBookTextAsync(subscriptionid, User.Identity.Name);
            SubscriptionViewModel = await _subscriptionviewmodelservice.GetUserSubscriptionViewModelAsync(User.Identity.Name);
        }
        public async Task OnPostDeleteSubscriptionAsync(Guid subscriptionid)
        {
            await _subscriptionviewmodelservice.DeleteSubscriptionAsync(subscriptionid);

            SubscriptionViewModel = await _subscriptionviewmodelservice.GetUserSubscriptionViewModelAsync(User.Identity.Name);
        }
    }
}
