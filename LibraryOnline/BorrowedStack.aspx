<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BorrowedStack.aspx.cs" Inherits="LibraryOnline.BorrowedStack1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="BorrowedStackTitle" runat="server" class="ContentHead"><h1>Выбранные книги</h1></div>
    <asp:GridView ID="StackList" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="Vertical" CellPadding="4"
        ItemType="LibraryOnline.Models.StackItem" SelectMethod="GetBorrowedStackItems" 
        CssClass="table table-striped table-bordered" >   
        <Columns>
        <asp:BoundField DataField="BookId" HeaderText="ID" SortExpression="BookId" />        
        <asp:BoundField DataField="BookItem.BookName" HeaderText="BookName" />        
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
        <asp:TemplateField HeaderText="Remove Item">            
                <ItemTemplate>
                    <asp:CheckBox id="Remove" runat="server"></asp:CheckBox>
                </ItemTemplate>        
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Взять книги">            
                <ItemTemplate>
                    <asp:CheckBox id="Borrow" runat="server" Checked="true"></asp:CheckBox>
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
        <asp:Button ID="UpdateBtn" runat="server" Text="Обновить" OnClick="UpdateBtn_Click" />
      </td>
      <td>
          <asp:Button ID="BorroweBtn" runat="server" Text="Взять книги" OnClick="BorroweBtn_Click" />
        <!--Checkout Placeholder -->
      </td>
    </tr>
    </table>
</asp:Content>
