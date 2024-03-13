using ApiTutorial.WebApi.Data;
using ApiTutorial.WebApi.DTOS;
using ApiTutorial.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTutorial.WebApi.Services
{
    public class BookService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        
        public async Task<bool> CreateBookAsync(BookDTO bookDTO)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == bookDTO.AuthorName.ToLower()
                && a.Lastname.ToLower() == bookDTO.AuthorLastname.ToLower()); // Ищем автора с теми данными, которые мы ввели
            if (author == null)
            {
                // Если автора не сущетсвует - создаём его
                await _context.Authors.AddAsync(new Entities.Author
                {
                    Name = bookDTO.AuthorName,
                    Lastname = bookDTO.AuthorLastname,
                    Surname = bookDTO.AuthorSurname
                });
                await _context.SaveChangesAsync();
                // Асинхронно добавляем автора и сохраняем изменения
                var authorNew = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == bookDTO.AuthorName.ToLower()
                    && a.Lastname.ToLower() == bookDTO.AuthorLastname.ToLower()); // Ищем только что добавленного автора
                // Асинхронно добавляем книгу и сохраняем изменения
                await _context.Books.AddAsync(new Book()
                {
                    Name = bookDTO.BookName,
                    AuthorId = authorNew.Id
                });
                await _context.SaveChangesAsync();
            }
            // Иначе
            else
            {
                var bookModel = new Book()
                {
                    Name = bookDTO.BookName,
                    AuthorId = author.Id
                }; // Создаём модель книги
                _context.Books.Add(bookModel); // Добавляем модель в нашу БД
                await _context.SaveChangesAsync(); // Сохраняем БД
            }
            return true; // Возвращаем булевское значение, т.к Task<bool>
        }
    }
}
