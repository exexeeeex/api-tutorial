using ApiTutorial.WebApi.DTOS;
using ApiTutorial.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTutorial.WebApi.Controllers
{
    [ApiController] // Указываем, что класс яв-ся контроллером
    [Route("/api/books")] // Указываем путь, по которому будет доступен контроллер
    public class BookController(BookService bookService) : ControllerBase // Наследуемся 
    {
        private readonly BookService _bookService = bookService;

        [HttpPost("createbook")]
        public async Task<IActionResult> CreateBook(BookDTO bookDTO) =>
            Ok(await _bookService.CreateBookAsync(bookDTO));
    }
}
