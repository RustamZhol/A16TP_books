using System.ComponentModel.DataAnnotations;

namespace TP_A16Book_Store.Models
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Authors(string name)
        {
            Name = name;
        }
    }
}
