using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Logic
{
    public class BorrowedBookActions : IDisposable
    {
        public string BorrowedStackId { get; set; }

        private BookContext _db = new BookContext();

        public const string StackSessionKey = "StackId";

        public string GetStackId()
        {
            if (HttpContext.Current.Session[StackSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[StackSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempStackId = Guid.NewGuid();
                    HttpContext.Current.Session[StackSessionKey] = tempStackId.ToString();
                }
            }
            return HttpContext.Current.Session[StackSessionKey].ToString();
        }

        public List<BorrowedBooks> GetBooksItems()
        {
            BorrowedStackId = GetStackId();

            return _db.Borroweds.Where(
                c => c.StackId == BorrowedStackId).ToList();
        }

        public List<BorrowedBooks> GetStackItems()
        {
            BorrowedStackId = GetStackId();

            return _db.Borroweds.Where(
                c => c.StackId == BorrowedStackId).ToList();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public void UpdateBorrowedBooksDatabase(String stackId, BorrowedBooksUpdates[] BookItemUpdates)
        {
            using (var db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    int StackItemCount = BookItemUpdates.Count();
                    List<BorrowedBooks> myBooks = GetBooksItems();
                    foreach (var bookItem in myBooks)
                    {
                        for (int i = 0; i < StackItemCount; i++)
                        {
                            if (bookItem.BorrowedBook.BookId == BookItemUpdates[i].BookId)
                            {
                                if ( BookItemUpdates[i].BackItem == true)
                                {
                                     BackItem(stackId, bookItem.BookId);
                                }
                                else
                                {
                                    UpdateItem(stackId, bookItem.BookId, BookItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Stack Database - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void BackItem(string backStackId, int backBookId)
        {
            using (var _db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    var myItem = (from c in _db.Borroweds where c.StackId == backStackId && c.BorrowedBook.BookId == backBookId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                         myItem.BorrowedBook.BookCount += myItem.Quantity;
                        _db.Borroweds.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Stack Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void UpdateItem(string updateStackId, int updateBookId, int quantity)
        {
            using (var _db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    var myItem = (from c in _db.Borroweds where c.StackId == updateStackId && c.BorrowedBook.BookId == updateBookId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        //myItem.BorrowedBook.BookCount -= myItem.Quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Stack Item - " + exp.Message.ToString(), exp);
                }
            }
        }


        public struct BorrowedBooksUpdates
        {
            public int BookId;
            public int PurchaseQuantity;
            public bool BackItem;
        }

    }
}