using LibraryOnline.Logic;
using LibraryOnline.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace LibraryOnline
{
    public partial class BorrowedStack1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (BorrowedStackActions usersBorrowedStack = new BorrowedStackActions())
            {
                decimal stackTotal = 0;
                stackTotal = usersBorrowedStack.GetTotal();
                if (stackTotal > 0)
                {
                    // Display Total.
                    lblTotal.Text = String.Format("{0:0}", stackTotal);
                }
                else
                {
                    LabelTotalText.Text = "";
                    lblTotal.Text = "";
                    BorrowedStackTitle.InnerText = "Stack is Empty";
                }
            }
        }

        public List<StackItem> GetBorrowedStackItems()
        {
            BorrowedStackActions actions = new BorrowedStackActions();
            return actions.GetStackItems();
        }

        public void BorrowedBookItems()
        {
            using (BorrowedStackActions usersBorrowedStack = new BorrowedStackActions())
            {
                String stackId = usersBorrowedStack.GetStackId();

                BorrowedStackActions.BorrowedBooksUpdates[] bookUpdates = new BorrowedStackActions.BorrowedBooksUpdates[StackList.Rows.Count];
                BorrowedStackActions.BorrowedStackUpdates[] stackUpdates = new BorrowedStackActions.BorrowedStackUpdates[StackList.Rows.Count];
                for (int i = 0; i < StackList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(StackList.Rows[i]);
                    bookUpdates[i].BookId = Convert.ToInt32(rowValues["BookId"]);
                    stackUpdates[i].BookId = Convert.ToInt32(rowValues["BookId"]);

                    CheckBox cbBack = new CheckBox();
                    cbBack = (CheckBox)StackList.Rows[i].FindControl("Borrow");
                    bookUpdates[i].BackItem = cbBack.Checked;
                    stackUpdates[i].RemoveItem = cbBack.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)StackList.Rows[i].FindControl("PurchaseQuantity");
                    bookUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                    stackUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                usersBorrowedStack.AddToBorrowedBook(stackId, bookUpdates);
                usersBorrowedStack.UpdateTackingStackDatabase(stackId, stackUpdates);
                StackList.DataBind();
                lblTotal.Text = String.Format("{0:c}", usersBorrowedStack.GetTotal());
                Response.Redirect("BorrowedBooksList.aspx");
                //return usersBorrowedStack.GetStackItems();

            }
        }

        public List<StackItem> UpdateStackItems()
        {
            using (BorrowedStackActions usersBorrowedStack = new BorrowedStackActions())
            {
                String stackId = usersBorrowedStack.GetStackId();

                BorrowedStackActions.BorrowedStackUpdates[] stackUpdates = new BorrowedStackActions.BorrowedStackUpdates[StackList.Rows.Count];
                for (int i = 0; i < StackList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(StackList.Rows[i]);
                    stackUpdates[i].BookId = Convert.ToInt32(rowValues["BookId"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)StackList.Rows[i].FindControl("Remove");
                    stackUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)StackList.Rows[i].FindControl("PurchaseQuantity");
                    stackUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                usersBorrowedStack.UpdateTackingStackDatabase(stackId, stackUpdates);
                StackList.DataBind();
                lblTotal.Text = String.Format("{0:c}", usersBorrowedStack.GetTotal());
                return usersBorrowedStack.GetStackItems();
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

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateStackItems();
        }

        protected void BorroweBtn_Click(object sender, EventArgs e)
        {
            BorrowedBookItems();
        }
    }
}