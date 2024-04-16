using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _24hr_Code_Challenge.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("time")]
        public string? Time { get; set; }
    }
}