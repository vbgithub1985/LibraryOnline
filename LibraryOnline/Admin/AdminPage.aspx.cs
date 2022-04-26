using LibraryOnline.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["BookAction"];
            if (productAction == "add")
            {
                LabelAddStatus.Text = "Book added!";
            }

            if (productAction == "remove")
            {
                LabelRemoveStatus.Text = "Book removed!";
            }

        }

        protected void AddBookButton_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (BookImagePath.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(BookImagePath.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    // Save to Images folder.
                    BookImagePath.PostedFile.SaveAs(path + BookImagePath.FileName);
                    // Save to Images/Thumbs folder.
                    BookImagePath.PostedFile.SaveAs(path + "Thumbs/" + BookImagePath.FileName);
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                // Add product data to DB.
                AddBooks books = new AddBooks();
                bool addSuccess = books.AddBook(AddBookAuthor.Text, AddBookName.Text, AddBookSummary.Text,
                    AddBookCount.Text, DropDownAddCategory.SelectedValue, BookImagePath.FileName);
                if (addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?BookAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Unable to add new product to database.";
                }
            }
            else
            {
                LabelAddStatus.Text = "Unable to accept file type.";
            }
        }

        public IQueryable GetCategories()
        {
            var _db = new Models.BookContext();
            IQueryable query = _db.Categories;
            return query;
        }

        public IQueryable GetBooks()
        {
            var _db = new Models.BookContext();
            IQueryable query = _db.Books;
            return query;
        }

        protected void RemoveBookButton_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.BookContext())
            {
                int bookId = Convert.ToInt16(DropDownRemoveBook.SelectedValue);
                var myItem = (from c in _db.Books where c.BookId == bookId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.Books.Remove(myItem);
                    _db.SaveChanges();

                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?BookAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Unable to locate book.";
                }
            }
        }
    }
}