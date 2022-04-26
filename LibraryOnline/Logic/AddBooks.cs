using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Logic
{
    public class AddBooks
    {
        public bool AddBook(string BookAuthor, string BookName, string BookSummary, string BookCount, string BookCategory, string BookImagePath)
        {
            var myBook = new Book();
            myBook.BookAuthor = BookAuthor;
            myBook.BookName = BookName;
            myBook.Summary = BookSummary;
            myBook.BookCount = Convert.ToInt32(BookCount);
            myBook.ImagePath = BookImagePath;
            myBook.BookCategoryId = Convert.ToInt32(BookCategory);

            using (BookContext _db = new BookContext())
            {
                // Add product to DB.
                _db.Books.Add(myBook);
                _db.SaveChanges();
            }
            // Success.
            return true;
        }
    }
}