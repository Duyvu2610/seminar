using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace seminar.Data
{
    [Table("Pen")]
    public class Pen
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Color { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}

