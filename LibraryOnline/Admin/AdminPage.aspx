<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="LibraryOnline.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administration</h1>
    <hr />
    <h3>Добавить книгу:</h3>
    <table>
        <tr>
            <td><asp:Label ID="LabelAddCategory" runat="server">Категория:</asp:Label></td>
            <td>
                <asp:DropDownList ID="DropDownAddCategory" runat="server" 
                    ItemType="LibraryOnline.Models.BookCategory" 
                    SelectMethod="GetCategories" DataTextField="BookCategoryName" 
                    DataValueField="BookCategoryId" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LableAddAuthor" runat="server">Автор:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddBookAuthor" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="* Book author required." ControlToValidate="AddBookAuthor" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddName" runat="server">Название книги:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddBookName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Book name required." ControlToValidate="AddBookName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddSummary" runat="server">Краткое описание:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddBookSummary" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="* Description required." ControlToValidate="AddBookSummary" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddCount" runat="server">Кол-во в наличии:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddBookCount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* Count required." ControlToValidate="AddBookCount" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Must be a valid count without $." ControlToValidate="AddBookCount" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddImageFile" runat="server">Image File:</asp:Label></td>
            <td>
                <asp:FileUpload ID="BookImagePath" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="* Image path required." ControlToValidate="BookImagePath" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <p></p>
    <p></p>
    <asp:Button ID="AddBookButton" runat="server" Text="Add Book" OnClick="AddBookButton_Click"  CausesValidation="true"/>
    <asp:Label ID="LabelAddStatus" runat="server" Text=""></asp:Label>
    <p></p>
    <h3>Remove Book:</h3>
    <table>
        <tr>
            <td><asp:Label ID="LabelRemoveBook" runat="server">Book:</asp:Label></td>
            <td><asp:DropDownList ID="DropDownRemoveBook" runat="server" ItemType="LibraryOnline.Models.Book" 
                    SelectMethod="GetBooks" AppendDataBoundItems="true" 
                    DataTextField="BookName" DataValueField="BookID" >
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <p></p>
    <asp:Button ID="RemoveBookButton" runat="server" Text="Remove Book" OnClick="RemoveBookButton_Click" CausesValidation="false"/>
    <asp:Label ID="LabelRemoveStatus" runat="server" Text=""></asp:Label>
</asp:Content>
