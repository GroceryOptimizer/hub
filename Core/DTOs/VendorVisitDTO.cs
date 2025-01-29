namespace Core.DTOs
{
    public class VendorVisitDTO
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public CoordinatesDTO VendorCoordinates { get; set; }
        public ShoppingCartDTO ShoppingCart { get; set; }
        public int TotalPrice { get; set; }
    }
}
