
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class StockItem
    {
        public int Quantity { get; set; }
        public int Price { get; set; }

        [Required]
        public Product Product { get; set; } = null!;
    }
}