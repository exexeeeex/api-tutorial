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
}
