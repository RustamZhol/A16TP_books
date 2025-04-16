using System.ComponentModel.DataAnnotations;

namespace TP_A16Book_Store.Models
{
    public class Types
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Types(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
