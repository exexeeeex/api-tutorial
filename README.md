# Начало
 
<h1>Добро пожаловать в мой туторил по созданию Api!</h1>

<p>Нам понадобится:</p>

> Руки
> Глаза
> Мозг
> C#, Dotnet, Visual Studio/Rider/Vs Code/Установленные пакеты (ASP.NET)

<p>Для начала создаём проект WebApi. Сделать это можно двумя способами: </p>
<li>Через консоль</li>
<li>Через IDE</li>

<p>Рассмотрим оба способа.</p>

## CONSOLE

<code>
cd FOLDER_PATH // путь до вашей папки
dotnet new webapi // создание шаблона WebApi
dotnet new sln // создание решения
</code>

<p>После этого в решении (VS/Rider) нужно будет прикрутить проект WebApi к решению. Как это сделать показано ниже: </p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_add_project.png"/>
<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_add_project_2.png"/>

## Visual Studio

<p>Создаём новый проект WebApi, ждём, радуемся!</p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_create_project.png"/>
<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_create_project_2.png"/>

# Смотрим то, что имеем

<p>Перед нашими глазами шаблон самой простой апишки, давайте разберёмся, что тут вообще есть: </p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_arh.png"/>

<li>Properties - настройки проекта</li>
<li>Controllers - контроллеры, на которые отправляются запросы, в них выполняются все методы, которые мы напишем.</li>
> Важное отступление! Мы можем писать методы как и в контроллерах, так и в отдельных сервисах, после вызывая их в контроллерах.
<li>Appsettings.json - файлик, который хранит информацию, с которой взаимодействует наш проект</li>
<li>Program.cs - исполняемый файл, в котором указаны "настройки" проекта.</li>
<li>WeatherForecast - мусор, удаляем сразу.</li>

## Пытаемся быть похожими на самураев и выбираем свой путь.

<p>Сейчас нужно определиться с масштабами нашего приложения.</p> 
<p>Если оно на выходе получится нереально огромное, то самое время сворачивать на микросервисы, ведь потом так будет проще.</p>
<p>Если наше приложение будет поменьше - разделяем его на доменную зону, зону приложения и саму апишку</p>
<p>Если наше приложение будет капец маленьким - оставляем всё так.</p>

<p>Разберём паример капец маленького приложения, но на моём гитхабе есть пример среднего приложения, а скоро появится пример большого приложения (нереально огромное)</p>
<a href="https://github.com/exexeeeex/Funny-Cocktail">Среднее приложение</a>

# А дальше что

<p>Хорошо, для начала избавимся от всего мусора и удалим ненужные контроллеры и файлы, в сухом остатке должно быть так: </p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_clear.png"/>

<p>Теперь определимся с идеей приложения.. Магазин книг!</p>

## База данных

<p>Все понимают, что база данных должна лежать на сервере, пусть даже и на локальном. Вы уже выбрали БД для себя? Postgres - отличный выбор!</p>

<p>Бежим в appsettings.json и добавляем строку подключения!</p>

<h2>"ConnectionStrings" : { "NAME": "your_connection_string" },</h2>

<p>Если вы выбрали Postgres, то ваша строка будет выглядеть как-то так:</p>

<h2>Host=localhost;Port=5432;Database=bookshop;Username=postgres;Password=root</h2>

> Замените поля Database, Username, Password на нужные вам поля.

<h1>Вы не выбрали Postgre?</h1>

<code>Server=SERVER_NAME;Database=bookshop;Trusted_Connection=true;Encrypt=false // строка для MS SQL
server=localhost;user=root;password=123456789;database=bookshop // строка для MySQL</code>

<p>Отлично, теперь нужно прикрутить базу данных к проекту!</p>

## Data, Entities и GetConnectionString

<p>Для начала устнановим все нужные Nuget пакеты.</p>

<h2>Microsoft.EntityFrameworkCore<br/>
Microsoft.EntityFrameworkCore.Design
Npgsql.EntityFrameworkCore.PostgreSQL // для Postgre
Microsoft.EntityFrameworkCore.SqlServer // для MS SQL</h2>

> Помните, что версию пакета надо устанавливать для версии своего проекта (7 - 7.0.17/8 - 8.0.3)

<p>Всё? Установили? Теперь создадим несколько папок</p>

> Data, Entities, Services, DTOS

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/screen_add_folder.png"/>

## Придумываем таблички!

<p>Сделаем простенькую БД, без лишних заморочек. В книжном должны быть:</p>

<li>Книги</li>
<li>Авторы</li>

<p>Создаём в папке Entities два файла: Author, Book</p>

<h1>Author</h1>
<code>namespace ApiTutorial.WebApi.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Имя
        public string? Lastname { get; set; } // Фамилия
        public string? Surname { get; set; } // Отчество
    }
}
</code>

<h1>Book</h1>
<code>
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
</code>

<h1>ЧУДЕСНО!</h1>

<p>В папке Data создаём файл ApplicationDbContext. В нём мы наследуемся от DbContext и объявляем наши Сущности</p>
<p>После объявления сущностей раскрываем конструктор, в который передаём настройки контекста и наследуемся от базового класса</p>
<p>В конечном итоге файл должен выглядеть так:</p>
<code>
 using ApiTutorial.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTutorial.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors => Set<Author>(); // показали БД, что у нас существуют авторы
        public DbSet<Book> Books => Set<Book>(); // показали БД, что у нас существуют книги

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } //
    }
}</code>

не забывай дышать

## Контекст объявили, сущности добавили, а дальше?

<h1>Бежим в Program.cs</h1>
<p>В нём нам нужно добавить контекст базы данных и строку подключения.</p>
<p>Сразу после var builder = WebApplication.CreateBuilder(args); пишем: </p>
<h2>builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));</h2>

> Замените UseNpgsql на своего провайдера БД (MS SQL - UseSqlServer; MySql - UseMySQL)

<p>Чудесно, осталось только сделать миграцию и любоваться нашей БД!</p>
<p>Открываем диспетчер пакетов и пишем:</p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/nuget_disp.png"/>

<code>
add-migration
MIGRATION_NAME
update-database</code

<p>Заходим в средство просмотра и любуемся.. Всё получилось? Великолепно!</p>

<img src="https://github.com/exexeeeex/api-tutorial/blob/main/screenshots/database.png"/>

# Пишем первый метод!

<p>Давайте напишем простенький метод, который будет принимать книгу и её автора, а поссле добавлять это всё в нашу базу данных</p>

<p>В папке DTOS создаём файл BookDTO</p>
<code>
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
</code>

<p>В папке Services создаём файл BookService, в котором раскрываем конструктор и передаём в него наш контекст базы данных</p>
<code>
 using ApiTutorial.WebApi.Data;

namespace ApiTutorial.WebApi.Services
{
    public class BookService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
    }
}
</code>

<p>Теперь создаём метод и прописываем логику<p>
<code>
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
</code>

<p>Чудесно! Теперь пропишем контроллер и насладимся нашей Api</p>
<p>В папке Controllers создаём BookController</p>
<code>
 using Microsoft.AspNetCore.Mvc;

namespace ApiTutorial.WebApi.Controllers
{
    [ApiController] // Указываем, что класс яв-ся контроллером
    [Route("/api/books")] // Указываем путь, по которому будет доступен контроллер
    public class BookController : ControllerBase // Наследуемся 
    {
    }
}
</code>

<p>Теперь передадим сюда наш сервис и вызовем его</p>
<code>
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
</code>

<p>Также нужно инициализировать наш сервис в Program.cs. Сразу после // Add services to the container прописываем:</p>
<h2>builder.Services.AddScoped<BookService>();</h2>

<p>Готово! Можете запускать проект и наслаждаться результатом..</p>
