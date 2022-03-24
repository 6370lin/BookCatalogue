using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class BookCatalogDbContextSeed
    {
        public static async Task SeedAsync(BookCatalogDbContext bookCatalogDbContext,
                                           ILogger logger)
        {
            try
            {
                if (!await bookCatalogDbContext.Books.AnyAsync())
                {
                    await bookCatalogDbContext.Books.AddRangeAsync(
                        GetPreconfiguredBooks());

                    await bookCatalogDbContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }

        static List<Book> GetPreconfiguredBooks()
        {
            return new List<Book>
            {
                new("ISBN", "description", "title", "long_text", 12, "http://catalogbaseurltobereplaced/images/products/1.png")
            };
        }
    }
}
