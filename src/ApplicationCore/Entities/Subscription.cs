using ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Subscription : IAggregateRoot
    {
        [Key]
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public string BookIsbn { get; private set; }
        public Book Book { get; private set; }

        public Subscription(string userId, string bookIsbn)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            BookIsbn = bookIsbn;
        }
    }
}
