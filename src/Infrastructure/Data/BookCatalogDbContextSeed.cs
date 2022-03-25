using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }

        static List<Book> GetPreconfiguredBooks()
        {
            return new List<Book>
            {
                new("ISBN1", "Victor Matfield", "Victor: My Journey", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/victor_matfield.jpg"),
                new("ISBN2","Jen Sincero", "You are a Badass", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/you_are_a_badass.jpg"),
                new("ISBN3","Holly Black", "The Cruel Prince", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/the_cruel_prince.jpg"),
                new("ISBN4","Sarah J. Maas", "A Court of Mist and Fury", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/a_court_of_mist_and_fury.jpg"),
                new("ISBN5","Chloe Benjamin", "The Immortalists", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/the_immortalists.jpg"),
                new("ISBN6","Cassandra Clare", "City of Fallen Angels", GetBookText(), 200, "http://catalogbaseurltobereplaced/images/items/city_of_fallen_angels.jpg")
            };
        }

        static string GetBookText()
        {
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eget mollis justo, a lobortis sapien. Cras aliquet ante nec ipsum accumsan, at interdum urna efficitur. Duis pulvinar, dui vel placerat elementum, lorem quam facilisis ex, quis scelerisque dui elit id ligula. Nulla vitae tincidunt nisi. Proin sed consectetur justo. Nulla facilisi. Quisque feugiat, odio eu vulputate tempus, lectus erat facilisis nibh, sed placerat lorem eros at ipsum. Curabitur fermentum venenatis interdum.";
            text += "Mauris consequat elementum purus, porttitor suscipit augue volutpat a. Nullam ullamcorper tempus tempus. Vivamus tristique, ante eget consectetur congue, metus odio tincidunt velit, ut dignissim purus ipsum sit amet dolor. Aliquam erat volutpat.Quisque euismod, odio dignissim lobortis ultrices, ex urna viverra nisl, eget sollicitudin neque justo vel orci.Integer maximus sapien ut est semper viverra aliquet in mi.Proin id neque sed nisi rutrum eleifend in et arcu. Vestibulum dignissim sapien vitae justo vestibulum malesuada.Donec in accumsan nibh, ut sagittis felis. Curabitur sed condimentum eros. Sed bibendum felis neque, eu condimentum erat ultrices vel.";
            text += "Vivamus risus arcu, convallis nec tortor quis, facilisis gravida est. Vestibulum interdum mattis tellus eu pulvinar. Mauris semper velit sodales quam tincidunt, vel dictum risus ultricies.In bibendum vitae nisi at euismod. Curabitur euismod erat dapibus sem tincidunt, id finibus orci facilisis.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Sed rutrum congue turpis vitae euismod. Quisque eget feugiat ex, interdum bibendum ipsum.";
            text += "Nulla bibendum felis pellentesque, porta nisi nec, facilisis ipsum.Morbi fringilla mauris lacus, at vehicula odio tincidunt vitae. Fusce dignissim magna eros, id dictum neque pulvinar non. Sed interdum ipsum in ornare feugiat. In augue enim, faucibus at consectetur eget, viverra eu orci. Etiam rhoncus elit id nisl cursus, nec dignissim tortor viverra.In bibendum sed mauris nec volutpat. Quisque sit amet tincidunt quam.Duis id consectetur sapien. Sed dapibus erat eu nulla facilisis, nec porttitor tellus mollis.Pellentesque bibendum id lorem ac congue.";
            text += "Sed semper sem a neque iaculis laoreet.Donec vitae luctus metus, in eleifend purus. Nam interdum tincidunt ipsum ut tincidunt. Integer in convallis est, non fermentum elit. Aenean sit amet mauris mi.Phasellus commodo quam sit amet molestie faucibus.Ut posuere et ligula et tincidunt. Donec egestas velit at bibendum tempus. Etiam ante risus, bibendum et urna sit amet, eleifend aliquet risus.Fusce eget molestie ex. Curabitur sit amet congue dui.";

            return text;
        }
    }
}
