using Blog.Data;
using Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
{
    public class Seed
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category()
                    {
                        Name = "Akcja"
                    },
                    new Category()
                    {
                        Name = "Seriale"
                    },
                    new Category()
                    {
                        Name = "Komedie"
                    },
                    new Category()
                    {
                        Name = "Western"
                    },
                    new Category()
                    {
                        Name = "Biograficzny"
                    },
                    new Category()
                    {
                        Name = "Romans"
                    },
                    new Category()
                    {
                        Name = "Erotyk"
                    },
                    new Category()
                    {
                        Name = "Fantastyczny"
                    }
                };

                await context.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}
