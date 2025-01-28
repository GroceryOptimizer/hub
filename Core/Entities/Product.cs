namespace Core.Entities
{
    internal class Product
    {
        public int Id { get; set; }
        private string _name { get; }

        public Product(string name)
        {
            _name = name;
        }
    }
}
