using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.Subscriptions
{
    public class SubscriptionSpecification : Specification<Subscription>, ISingleResultSpecification
    {
        public SubscriptionSpecification(string userid, string bookid)
        {
            Query.Where(s => s.UserId == userid && s.BookIsbn == bookid);
        }
    }
}
