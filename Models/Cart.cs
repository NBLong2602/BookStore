namespace BookStore.Models
{
    public class Cart
    {
        public List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Book product, int quantity)
        {
            CartLine? line = lineCollection
            .Where(p => p.Book.Isbn == product.Isbn)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void ReduceItem(Book product, int quantity)
        {
            CartLine? line = lineCollection
            .Where(p => p.Book.Isbn == product.Isbn)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity -= quantity;
            }
        }
        public void RemoveLine(Book product) =>
            lineCollection.RemoveAll(l => l.Book.Isbn == product.Isbn);
        public decimal ComputeTotalValue() =>
            (decimal)lineCollection.Sum(e => e.Book?.Price * e.Quantity);
        public static string ConvertToVND(decimal Value)
        {
            decimal vndAmount = Value;
            return vndAmount.ToString("N0") + " VND"; // Định dạng số tiền VND với dấu phân cách hàng nghìn
        }
        public void Clear() => lineCollection.Clear();
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Book Book { get; set; } = new Book();
        public int Quantity { get; set; }
    }
}
