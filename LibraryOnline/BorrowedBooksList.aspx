<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BorrowedBooksList.aspx.cs" Inherits="LibraryOnline.BorrowedBooksList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="BorrowedBookTitle" runat="server" class="ContentHead"><h1>Взятые книги</h1></div>
    <asp:GridView ID="BorrowedList" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="Vertical" CellPadding="5"
        ItemType="LibraryOnline.Models.BorrowedBooks" SelectMethod="GetBorrowedBooksItems" 
        CssClass="table table-striped table-bordered" >   
        <Columns>
        <asp:BoundField DataField="BookId" HeaderText="ID" SortExpression="BookId" />
        <asp:BoundField DataField="BorrowedBook.BookAuthor" HeaderText="BookAuthor" />
        <asp:BoundField DataField="BorrowedBook.BookName" HeaderText="BookName" />        
        <%--<asp:BoundField DataField="Book.Count" HeaderText="Price (each)" DataFormatString="{0:c}"/>--%>     
        <asp:TemplateField   HeaderText="Quantity">            
                <ItemTemplate>
                    <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox> 
                </ItemTemplate>        
        </asp:TemplateField>    
        <%--<asp:TemplateField HeaderText="Item Total">            
                <ItemTemplate>
                    <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) *  Convert.ToDouble(Item.Product.UnitPrice)))%>
                </ItemTemplate>        
        </asp:TemplateField> --%>
        <asp:TemplateField HeaderText="Back Item">            
                <ItemTemplate>
                    <asp:CheckBox id="Back" runat="server"></asp:CheckBox>
                </ItemTemplate>        
        </asp:TemplateField>    
        </Columns>    
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelTotalText" runat="server" Text="Выбранные книги: "></asp:Label>
            <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
        </strong> 
    </div>
    <br />
    <table> 
    <tr>
      <td>
        <asp:Button ID="BackBtn" runat="server" Text="Отдать книги" OnClick="BackBtn_Click" />
      </td>
      <%--<td>
          <asp:Button ID="BorroweBtn" runat="server" Text="Взять книги" OnClick="BorroweBtn_Click" />
        <!--Checkout Placeholder -->
      </td>--%>
    </tr>
    </table>
</asp:Content>
