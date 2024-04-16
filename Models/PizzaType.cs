using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _24hr_Code_Challenge.Models
{
    [Table("pizza_types")]
    public class PizzaType
    {
        [Key]
        [Column("pizza_type_id")]
        public string? PizzaTypeId { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("category")]
        public string? Category { get; set; }
        [Column("ingredients")]
        public string? Ingredients { get; set; }
    }
}