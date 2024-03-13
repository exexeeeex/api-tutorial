namespace ApiTutorial.WebApi.DTOS
{
    public class BookDTO
    {
        public string? AuthorName { get; set; } // Имя автора
        public string? AuthorLastname { get; set; } // Фамилия автора
        public string? AuthorSurname { get; set; } // Отчество автора
        public string? BookName { get; set; } // Название книги
    }
}
