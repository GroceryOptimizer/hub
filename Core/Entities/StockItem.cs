namespace Core.Entities
{
    internal class StockItem
    {
        public int Id { get; set; }
        private Product _product { get; }
        private int _amount { get; }

        public StockItem(Product product, int amount)
        {
            _product = product;
            _amount = amount;
        }

    }
}
