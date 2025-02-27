using Bookly.APIs.Entities;
using System.Text.Json;

namespace Bookly.APIs.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task DataSeedAsync(ApplicationDbContext dbContext)
        {
            //if (!dbContext.Authors.Any())
            //{
            //    var authorsData = File.ReadAllText("../Bookly.APIs/Data/DataSeed/Authors.json");
            //    var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
            //    if (authors is not null && authors.Count > 0)
            //    {
            //        foreach (var author in authors)
            //            await dbContext.Authors.AddAsync(author);
            //    }
            //    await dbContext.SaveChangesAsync();
            //}
            //if (!dbContext.Books.Any())
            //{
            //    var booksData = File.ReadAllText("../Bookly.APIs/Data/DataSeed/Book.json");
            //    var books = JsonSerializer.Deserialize<List<Book>>(booksData);
            //    if(books is not null && books.Count > 0)
            //    {
            //        foreach (var book in books)
            //            await dbContext.Books.AddAsync(book);
            //    }
            //    await dbContext.SaveChangesAsync();
            //}

          

            if (!dbContext.Set<AuthorBook>().Any())
            {
                var authorBooksData = File.ReadAllText("../Bookly.APIs/Data/DataSeed/AuthorBook.json");
                var authorBooks = JsonSerializer.Deserialize<List<AuthorBook>>(authorBooksData);
                if (authorBooks is not null && authorBooks.Count > 0)
                {
                    foreach (var authorBook in authorBooks)
                        await dbContext.Set<AuthorBook>().AddAsync(authorBook);
                }
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
