using LibraryOnline.Logic;
using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline
{
    public partial class BorrowedBooksList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<BorrowedBooks> GetBorrowedBooksItems()
        {
            BorrowedBookActions actions = new BorrowedBookActions();
            return actions.GetBooksItems();
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            BackBooks();
        }

        public List<BorrowedBooks> BackBooks()
        {
            using (BorrowedBookActions usersBorrowedBook = new BorrowedBookActions())
            {
                String stackId = usersBorrowedBook.GetStackId();

                BorrowedBookActions.BorrowedBooksUpdates[] bookUpdates = new BorrowedBookActions.BorrowedBooksUpdates[BorrowedList.Rows.Count];
                for (int i = 0; i < BorrowedList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(BorrowedList.Rows[i]);
                    bookUpdates[i].BookId = Convert.ToInt32(rowValues["BookId"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)BorrowedList.Rows[i].FindControl("Back");
                    bookUpdates[i].BackItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)BorrowedList.Rows[i].FindControl("PurchaseQuantity");
                    bookUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                usersBorrowedBook.UpdateBorrowedBooksDatabase(stackId, bookUpdates);
                BorrowedList.DataBind();
                //lblTotal.Text = String.Format("{0:c}", usersBorrowedBook.GetTotal());
                return usersBorrowedBook.GetStackItems();
            }
        }

        

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    // Extract values from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }

    }
}