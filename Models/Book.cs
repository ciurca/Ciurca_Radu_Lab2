using System.ComponentModel.DataAnnotations.Schema;

namespace Ciurca_Radu_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? AuthorID { get; set; }
        public Authors? Author { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<PublishedBook> PublishedBooks { get; set; }

    }
}
