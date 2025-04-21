using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

/*Начать следует с:

Создание проекта ASP.NET Core.
Установка необходимых пакетов NuGet для Entity Framework Core.
Создание моделей C# для ваших таблиц.
Создание контекста базы данных.
Настройка строки подключения в appsettings.json.
Создание миграции для создания начальной схемы базы данных.*/

namespace TP_A16Book_Store.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public string Type { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public Books(){}
        public Books(int id, string title, string author, int year, string genre, string type, decimal price, string description, string imageUrl, int quantity)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            Type = type;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
            Quantity = quantity;
        }
    }
}
