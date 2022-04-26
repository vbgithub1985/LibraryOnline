using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Logic
{
    public class BorrowedStackActions : IDisposable
    {
        public string BorrowedStackId { get; set; }

        private BookContext _db = new BookContext();

        public const string StackSessionKey = "StackId";

        public void AddToBorrow(StackItem stackItem)
        {
            BorrowedStackId = GetStackId();
            var borrowedBookItem = _db.Borroweds.SingleOrDefault(
                c => c.StackId == BorrowedStackId
                && c.BookId == stackItem.BookId);
            if (borrowedBookItem == null)
            {
                borrowedBookItem = new BorrowedBooks
                {
                    BorrowedBookId = Guid.NewGuid().ToString(),
                    BookId = stackItem.BookId,
                    DateTaking = stackItem.DateTaking,
                    StackId = stackItem.StackId,
                    Quantity = stackItem.Quantity
                };
                _db.Borroweds.Add(borrowedBookItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                borrowedBookItem.Quantity+= stackItem.Quantity;
            }

            
            stackItem.BookItem.BookCount -= stackItem.Quantity;
            _db.SaveChanges();

            //BorrowedStackId = GetStackId();
            
            //if (borrowedBookItem == null)
            //{

            //}
        }

        public void AddToStack(int id)
        {
                     
            BorrowedStackId = GetStackId();

            var stackItem = _db.BorrowedStackItem.SingleOrDefault(
                c => c.StackId == BorrowedStackId
                && c.BookId == id);
            if (stackItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                stackItem = new StackItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    BookId = id,
                    StackId = BorrowedStackId,
                    BookItem = _db.Books.SingleOrDefault(
                   p => p.BookId == id),
                    Quantity = 1,
                    DateTaking = DateTime.Now
                };

                _db.BorrowedStackItem.Add(stackItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                stackItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

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

        public List<StackItem> GetStackItems()
        {
            BorrowedStackId = GetStackId();

            return _db.BorrowedStackItem.Where(
                c => c.StackId == BorrowedStackId).ToList();
        }

        public decimal GetTotal()
        {
            BorrowedStackId = GetStackId();
            decimal? total = decimal.Zero;
            total = (decimal?)(from stackItems in _db.BorrowedStackItem
                               where stackItems.StackId == BorrowedStackId
                               select (int?)stackItems.Quantity).Sum();
            return total ?? decimal.Zero;
        }

        public BorrowedStackActions GetStack(HttpContext context)
        {
            using (var stack = new BorrowedStackActions())
            {
                stack.BorrowedStackId = stack.GetStackId();
                return stack;
            }
        }

        public void UpdateTackingStackDatabase(String stackId, BorrowedStackUpdates[] StackItemUpdates)
        {
            using (var db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    int StackItemCount = StackItemUpdates.Count();
                    List<StackItem> myStack = GetStackItems();
                    foreach (var stackItem in myStack)
                    {
                        
                        for (int i = 0; i < StackItemCount; i++)
                        {
                            if (stackItem.BookItem.BookId == StackItemUpdates[i].BookId)
                            {
                                if (StackItemUpdates[i].PurchaseQuantity < 1 || StackItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(stackId, stackItem.BookId);
                                }
                                else
                                {
                                    UpdateItem(stackId, stackItem.BookId, StackItemUpdates[i].PurchaseQuantity);
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

        public void AddToBorrowedBook(String stackId, BorrowedBooksUpdates[] BorrowedBooksItem)
        {
            using (var db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    int StackItemCount = BorrowedBooksItem.Count();
                    List<StackItem> myStack = GetStackItems();
                    foreach (var stackItem in myStack)
                    {
                        for (int i = 0; i < StackItemCount; i++)
                        {
                            if (stackItem.BookItem.BookId == BorrowedBooksItem[i].BookId)
                            {
                                AddToBorrow(stackItem);
                                
                                
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Add BorrowedBooks Database - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void RemoveItem(string removeStackId, int removeBookId)
        {
            using (var _db = new LibraryOnline.Models.BookContext())
            {
                try
                {
                    var myItem = (from c in _db.BorrowedStackItem where c.StackId == removeStackId && c.BookItem.BookId == removeBookId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                         //myItem.BookItem.BookCount += myItem.Quantity;
                        _db.BorrowedStackItem.Remove(myItem);
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
                    var myItem = (from c in _db.BorrowedStackItem where c.StackId == updateStackId && c.BookItem.BookId == updateBookId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        //myItem.BookItem.BookCount -= myItem.Quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Stack Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void EmptyCart()
        {
            BorrowedStackId = GetStackId();
            var stackItems = _db.BorrowedStackItem.Where(
                c => c.StackId == BorrowedStackId);
            foreach (var stackItem in stackItems)
            {
                _db.BorrowedStackItem.Remove(stackItem);
            }
            // Save changes.             
            _db.SaveChanges();
        }

        public int GetCount()
        {
            BorrowedStackId = GetStackId();

            // Get the count of each item in the cart and sum them up          
            int? count = (from stackItems in _db.BorrowedStackItem
                          where stackItems.StackId == BorrowedStackId
                          select (int?)stackItems.Quantity).Sum();
            // Return 0 if all entries are null         
            return count ?? 0;
        }

        public struct BorrowedStackUpdates
        {
            public int BookId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }

        public struct BorrowedBooksUpdates
        {
            public int BookId;
            public int PurchaseQuantity;
            public bool BackItem;
        }
    }
}