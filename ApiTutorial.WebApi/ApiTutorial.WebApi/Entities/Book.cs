using System.Text.Json.Serialization;

namespace ApiTutorial.WebApi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Название книги
        public int AuthorId { get; set; } // Айдишник автора
        // Показываем БД, где и с чем будет связь (Стремление к 3НФ)
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
