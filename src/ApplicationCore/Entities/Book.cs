namespace ApplicationCore.Entities
{
    public class Book
    {
        public string Name { get; private set; }
        public string Text { get; private set; }
        private readonly double _purchaseprice;
        public string Purchaseprice => "R" + _purchaseprice.ToString();
    }
}
