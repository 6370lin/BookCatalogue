using ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class User : IAggregateRoot
    {
        [Key]
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => FirstName + " " + LastName;
        public ICollection<Subscription> Subscriptions { get; private set; }
        private decimal _monthlySubscription => Subscriptions.Sum(s => s.Book.SubscriptionPrice);
        public string MonthlySubscriptionDisplay => "R" + _monthlySubscription.ToString();

        public User(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Subscriptions = new List<Subscription>();
        }
    }
}
