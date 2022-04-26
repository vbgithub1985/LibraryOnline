using LibraryOnline.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline
{
    public partial class AddToStack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["BookId"];
            int bookId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out bookId))
            {
                using (BorrowedStackActions usersTakingStack = new BorrowedStackActions())
                {
                    usersTakingStack.AddToStack(Convert.ToInt16(rawId));
                }

            }
            else
            {
                Debug.Fail("ERROR : We should never get to AddToStack.aspx without a BookId.");
                throw new Exception("ERROR : It is illegal to load AddToStack.aspx without setting a StackId.");
            }
            Response.Redirect("BorrowedStack.aspx");
        }
    }
}