using System.ComponentModel.DataAnnotations;

namespace TP_A16Book_Store.Models
{
    public class Genres
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genres(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
