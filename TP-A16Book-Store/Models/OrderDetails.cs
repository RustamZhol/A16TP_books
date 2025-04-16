using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP_A16Book_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace TP_A16Book_Store.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public required Orders Order { get; set; }

        public required Books Book { get; set; }
    }
}
