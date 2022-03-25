using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ISubscriptionViewModelService
    {
        Task AddSubscription(string bookid, string userid);
        Task<SubscriptionViewModel> GetUserSubscriptionViewModelAsync(string userid);
    }
}
