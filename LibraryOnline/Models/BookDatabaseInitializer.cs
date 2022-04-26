using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models
{
    public class BookDatabaseInitializer : DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetBooks().ForEach(p => context.Books.Add(p));
        }

        private static List<BookCategory> GetCategories()
        {
            var categories = new List<BookCategory> {
                new BookCategory
                {
                    BookCategoryId = 1,
                    BookCategoryName = "Scientific literature"
                },
                new BookCategory
                {
                    BookCategoryId = 2,
                    BookCategoryName = "Fantasy"
                },
                new BookCategory
                {
                    BookCategoryId = 3,
                    BookCategoryName = "Fiction"
                }
            };

            return categories;
        }

        private static List<Book> GetBooks()
        {
            var books = new List<Book> {
                new Book
                {
                    BookId = 1,
                    BookName = "1984",
                    BookAuthor="George Orwell",
                    Summary = "The story takes place in an imagined future, the year 1984, when much of the world has " + 
                               "fallen victim to perpetual war, omnipresent government surveillance, historical negationism, and propaganda.",
                    ImagePath = "",
                    BookCount = 10,
                    BookCategoryId = 3
                },
                new Book
                {
                    BookId = 2,
                    BookName = "Animal Farm",
                    BookAuthor="George Orwell",
                    Summary = "Mr. Jones has been spoiled to the point of impossibility. Every day he overturns a glass" + 
                               "after a glass, forgetting about the household. Farm animals are tired of living in hunger. ",
                    ImagePath = "",
                    BookCount = 20,
                    BookCategoryId = 3
                },
                new Book
                {
                    BookId = 3,
                    BookName = "A Brief History of Time",
                    BookAuthor="Stephen Hawking",
                    Summary = "A landmark volume in science writing by one of the great minds of our time, Stephen Hawking’s " + 
                    "book explores such profound questions as: How did the universe begin—and what made its start possible?",
                    ImagePath = "",
                    BookCount = 10,
                    BookCategoryId = 1
                },
                new Book
                {
                    BookId = 4,
                    BookName = "Black holes and young universes",
                    BookAuthor="Stephen Hawking",
                    Summary = "",
                    ImagePath = "",
                    BookCount = 6,
                    BookCategoryId = 1
                },
                new Book
                {
                    BookId = 5,
                    BookName = "Hard to be god",
                    BookAuthor="Arkady Strugatsky, Boris Strugatsky",
                    Summary = "",
                    ImagePath = "",
                    BookCount = 8,
                    BookCategoryId = 2
                },
                new Book
                {
                    BookId = 6,
                    BookName = "Roadside Picnic",
                    BookAuthor="Arkady Strugatsky, Boris Strugatsky",
                    Summary = "",
                    ImagePath = "",
                    BookCount = 10,
                    BookCategoryId = 2
                }
            };

            return books;
        }
    }
}