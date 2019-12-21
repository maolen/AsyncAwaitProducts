using System.Collections.Generic;
using System.Data.Entity;

namespace AsyncAwaitProducts
{
    public class DbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            var defaultProducts = new List<Product>()
            {
                new Product
                {
                    Name = "Bread",
                    Price = 13,
                    Count = 5,
                },
                new Product
                {
                    Name = "Soap",
                    Price = 10,
                    Count = 10,
                },
                new Product
                {
                    Name = "Butter",
                    Price = 200,
                    Count = 5,
                }, new Product
                {
                    Name = "Milk",
                    Price = 130,
                    Count = 7,
                }
            }
            context.Products.AddRange(defaultProducts);
            context.Users.Add(new User { Login = "admin", Password = "admin" });
            base.Seed(context);
        }
    }
}
