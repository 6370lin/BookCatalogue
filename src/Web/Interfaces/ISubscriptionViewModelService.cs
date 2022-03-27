using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ISubscriptionViewModelService
    {
        Task AddSubscriptionAsync(string bookid, string userid);
        Task DeleteSubscriptionAsync(Guid subscriptionid);
        Task<string> GetBookTextAsync(Guid subscriptionid, string userid);
        Task<SubscriptionViewModel> GetUserSubscriptionViewModelAsync(string userid);
    }
}
