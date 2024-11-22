using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ciurca_Radu_Lab2.Models;

namespace Ciurca_Radu_Lab2.Data
{
    public class Ciurca_Radu_Lab2Context : DbContext
    {
        public Ciurca_Radu_Lab2Context (DbContextOptions<Ciurca_Radu_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Ciurca_Radu_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Ciurca_Radu_Lab2.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Ciurca_Radu_Lab2.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Ciurca_Radu_Lab2.Models.Authors> Authors { get; set; } = default!;
    }
}

