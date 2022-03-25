using Ardalis.Specification;

namespace ApplicationCore.Specifications.User
{
    public class UserWithSubscriptionSpecification : Specification<Entities.User>, ISingleResultSpecification
    {
        public UserWithSubscriptionSpecification(string userid)
        {
            Query.Where(u => u.Email == userid)
                .Include(u => u.Subscriptions)
                    .ThenInclude(s => s.Book);
        }
    }
}
