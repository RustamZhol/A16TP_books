using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TP_A16Book_Store.Models;

namespace TP_A16Book_Store.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public required string Status { get; set; }

        public required CustomUsers User { get; set; }
    }
}
