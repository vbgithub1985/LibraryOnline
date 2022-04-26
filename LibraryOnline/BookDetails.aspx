<%@ Page Title="Выбранная книга" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="BookDetails.aspx.cs" Inherits="LibraryOnline.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="BookDetails" runat="server" ItemType="LibraryOnline.Models.Book" SelectMethod ="GetBook" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                
                
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="/Catalog/Images/<%#:Item.ImagePath %>" style="border:solid; height:300px" alt="<%#:Item.BookName %>"/>
                    </td>
                    <td>&nbsp;</td>  
                    <td style="vertical-align: top; text-align:left;">
                        <b><%#:Item.BookName %> </b> <br /> <span> by <%#:Item.BookAuthor %></span> <br />
                        <b>Краткое описание:</b><br /><%#:Item.Summary %>
                        <br />
                        <span><b>Количество:</b>&nbsp;<%#: String.Format("{0:c}", Item.BookCount) %></span>
                        <br />
                        <span><b>Product Number:</b>&nbsp;<%#:Item.BookId %></span>
                        <br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
