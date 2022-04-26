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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Book> GetBook([QueryString("BookId")] int? BookId)
        {
            var _db = new LibraryOnline.Models.BookContext();
            IQueryable<Book> query = _db.Books;
            if (BookId.HasValue && BookId > 0)
            {
                query = query.Where(p => p.BookId == BookId);
            }
            else
            {
                query = null;
            }
            return query;
        }
    }
}