using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _24hr_Code_Challenge.Models
{
    [Table("pizzas")]
    public class Pizza
    {
        [Key]
        [Column("pizza_id")]
        public string? PizzaId { get; set; }
        [Column("pizza_type_id")]
        public string? PizzaTypeId { get; set; }
        [Column("size")]
        public string? Size { get; set; }
        [Column("price")]
        public double Price { get; set; }

        public PizzaType? PizzaType { get; set; }
    }
}
