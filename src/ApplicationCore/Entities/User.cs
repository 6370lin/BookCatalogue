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
        public User(string email)
        {
            Email = email;
            FirstName = "";
            LastName = "";
            Subscriptions = new List<Subscription>();
        }
        public User(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Subscriptions = new List<Subscription>();
        }
    }
}
