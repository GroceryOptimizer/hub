namespace Core.Entities
{
    internal class Vendor
    {
        public int Id { get; set; }
        private string _name { get; }
        private Coordinates _coordinates { get; }

        public Vendor(string name, Coordinates coordinates)
        {
            _name = name;
            _coordinates = coordinates;
        }
    }
}
