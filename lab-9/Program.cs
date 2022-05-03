using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            //Console.WriteLine(context.Books.Find(1));
            IQueryable<Book> books = from book in context.Books
            where book.EditionYear > 2019
            select book;
            Console.WriteLine(string.Join("\n", books));

            //klasyczny join
            var list = from book in context.Books
            join author in context.Authors
            on book.AuthorId equals author.Id
            where book.EditionYear > 2019
            select new { BookAuthor = author.Name, Title = book.Title }; //obiekt klasy anonimowej
            Console.WriteLine(string.Join("\n", list));

            //fluent api join
            list = context.Authors.Join(
                context.Books,
                a => a.Id,
                b => b.AuthorId,
                (a, b) => new { BookAuthor = a.Name, Title = b.Title }
                );

            foreach (var item in list)
            {
                Console.WriteLine(item.BookAuthor);
            }
        }
    }

    public record Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EditionYear { get; set; }
        public int AuthorId { get; set; }
    }

    //1. Zarejestruj encje w kontekscie
    //2. Dodaj dwa egzemplarze ksiazki o id = 1
    //3. usun plik bazy i uruchom program
    //4. wykonaj zlaczenie dla kazdego egzemplarza any uzyskac liste obiektow z unuqueNumber i Title

    public record BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UniqueNumber { get; set; }
    }

    public record Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class AppContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Author> BookCopy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE=d:\\Users\\mateusz.szymanski\\database\\base.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .ToTable("books")
                .HasData(
                new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C#" },
                new Book() { Id = 2, AuthorId = 1, EditionYear = 2021, Title = "Asp.Net" },
                new Book() { Id = 3, AuthorId = 2, EditionYear = 2019, Title = "Data structure" },
                new Book() { Id = 4, AuthorId = 2, EditionYear = 2018, Title = "CWeb application" }
                );
            modelBuilder.Entity<Author>()
                .ToTable("authors")
                .HasData(
                new Author() { Id = 1, Name = "Freeman" },
                new Author() { Id = 2, Name = "Bloch" }
                );
            modelBuilder.Entity<Author>()
                .ToTable("copies")
                .HasData(
                new BookCopy() { Id = 1, BookId=1, UniqueNumber = 1 },
                new BookCopy() { Id = 2, BookId = 1, UniqueNumber = 2 }
                );
        }
    }
}
