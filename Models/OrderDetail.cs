using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _24hr_Code_Challenge.Models
{
    [Table("order_details")]
    public class OrderDetail
    {
        [Key]
        [Column("order_details_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey("Order")]
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order? Order { get; set; } // Reference to Order

        [ForeignKey("Pizza")]
        [Column("pizza_id")]
        public string? PizzaId { get; set; }
        public Pizza? Pizza { get; set; } // Reference to Pizza
    }
}