namespace PublicApi.Requests
{
    public class AddBookRequest
    {
        public string Isbn { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string subscriptionPrice { get; set; }
        public string pictureUri { get; set; }
    }
}
