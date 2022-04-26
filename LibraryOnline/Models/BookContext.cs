using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("LibraryOnline")
        {
        }
        public DbSet<BookCategory> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<StackItem> BorrowedStackItem { get; set; }
        public DbSet<BorrowedBooks> Borroweds { get; set; }
    }
}