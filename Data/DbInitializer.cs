using Ciurca_Radu_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Ciurca_Radu_Lab2.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Ciurca_Radu_Lab2Context(serviceProvider.GetRequiredService<DbContextOptions<Ciurca_Radu_Lab2Context>>()))
            {
                if (context.Book.Any())
                {
                    return; // BD a fost creata anterior
                }
                context.Authors.AddRange(
                    new Authors
                    {
                        FirstName = "Mihail",
                        LastName = "Sadoveanu",
                    },
                    new Authors
                    {
                        FirstName = "George",
                        LastName = "Calinescu",
                    },
                    new Authors
                    {
                        FirstName = "Mircea",
                        LastName = "Eliade",
                    }
                    );
                context.Book.AddRange(
                new Book
                {
                    Title = "Baltagul",
                    //Author = context.Authors,
                    Price=Decimal.Parse("22")},
                new Book
                {
                    Title = "Enigma Otiliei",
                    //Author = context.Authors.Find(2),
                    Price=Decimal.Parse("18")},
                new Book
                {
                    Title = "Maytrei",
                    //Author = context.Authors.Find(3),
                    Price=Decimal.Parse("27")}
               
                );

                context.Genre.AddRange(
               new Genre { Name = "Roman" },
               new Genre { Name = "Nuvela" },
               new Genre { Name = "Poezie" }
                );
                context.Customer.AddRange(
                new Customer
                {
                    Name = "Popescu Marcela",
                    Adress = "Str. Plopilor, nr. 24",
                    BirthDate = DateTime.Parse("1979-09-01")
                },
                new Customer
                {
                    Name = "Mihailescu Cornel",
                    Adress = "Str. Bucuresti, nr. 45, ap. 2",
                    BirthDate=DateTime.Parse("1969 - 07 - 08")}
                );

                context.SaveChanges();
            }
        }
    }
}
