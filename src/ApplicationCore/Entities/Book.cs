using ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Book : IAggregateRoot
    {
        [Key]
        public string Isbn { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string PictureUri { get; private set; }
        public ICollection<Subscription> Subscriptions { get; private set; }
        public decimal SubscriptionPrice { get; private set; }
        public string SubscriptionPriceDisplay => "R" + SubscriptionPrice.ToString();

        public Book(string isbn,
            string description, 
            string title, 
            string text, 
            decimal subscriptionPrice,
            string pictureUri)
        {
            Isbn = isbn;
            Description = description;
            Title = title;
            Text = text;
            SubscriptionPrice = subscriptionPrice;
            PictureUri = pictureUri;
        }
    }
}
