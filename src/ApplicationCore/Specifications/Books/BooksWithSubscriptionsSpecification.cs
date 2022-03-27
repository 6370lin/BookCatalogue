using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.Books
{
    public class BooksWithSubscriptionsSpecification : Specification<Book>
    {
        public BooksWithSubscriptionsSpecification()
        {
            Query
               .Include(b => b.Subscriptions);
        }
    }
}
