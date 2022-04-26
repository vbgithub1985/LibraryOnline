using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline
{
    public partial class BookList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            //if (HttpContext.Current.User.IsInRole("canEdit"))
            //{
            //    ListViewItem item = BookLists.;
            //    HyperLink hl = (HyperLink)item.FindControl("editBookAdmin");
            //    hl.Visible = true;
            //}
            //if (HttpContext.Current.User.IsInRole("canEdit"))
            //{
            //    HyperLink hl = (HyperLink)BookLists.FindControl("editBookAdmin");
            //    hl.Visible = true;
            //    //editBookAdmin.Visible = true;
            //}
        }

        public void BookLists_OnItemEditing(Object sender, ListViewEditEventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("canEdit"))
            {
                ListViewItem item = BookLists.Items[e.NewEditIndex];
                HyperLink hl = (HyperLink)item.FindControl("editBookAdmin");
                hl.Visible = true;
            }
                
        }

        
        public IQueryable<Book> GetBooks([QueryString("id")] int? categoryId)
        {
            var _db = new LibraryOnline.Models.BookContext();
            IQueryable<Book> query = _db.Books;
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.BookCategoryId == categoryId);
            }
            return query;
        }
    }
}