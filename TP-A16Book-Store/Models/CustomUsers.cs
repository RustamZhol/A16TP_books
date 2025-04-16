using System.ComponentModel.DataAnnotations;

namespace TP_A16Book_Store.Models
{
    public class CustomUsers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public CustomUsers(int id, string username, string password, string email, string address)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Address = address;
        }
    }
}
