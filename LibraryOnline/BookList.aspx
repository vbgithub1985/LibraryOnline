<%@ Page Title="Книги" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="BookList.aspx.cs" Inherits="LibraryOnline.BookList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                <h2><%: Page.Title %></h2>
            </hgroup>

            <asp:ListView ID="BookLists" runat="server" 
                DataKeyNames="BookId" GroupItemCount="3"
                ItemType="LibraryOnline.Models.Book" SelectMethod="GetBooks"
                OnItemEditing="BookLists_OnItemEditing">
                <EmptyDataTemplate>
                    <table >
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td/>
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <a href="BookDetails.aspx?BookId=<%#:Item.BookId%>">
                                        <img src="/Catalog/Images/Thumbs/<%#:Item.ImagePath%>"
                                            width="100" height="75" style="border: solid" />
                                        
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        <%#:Item.BookAuthor%>
                                    </span>
                                    <br />
                                    <a href="BookDetails.aspx?BookId=<%#:Item.BookId%>">
                                        
                                        <span>
                                            <%#:Item.BookName%>
                                        </span>
                                         
                                    </a>
                                    <br />
                                    <span>
                                        <b>В наличии: </b><%#:String.Format("{0:c}", Item.BookCount)%>
                                    </span>
                                    <br />
                                    

                                    <a href="/AddToStack.aspx?BookId=<%#:Item.BookId %>">               
                                        <span class="BookListItem">
                                            <b>Взять книгу<b>
                                        </span>           
                                    </a>
                                    <br>
                                    <a runat="server" ID ="editBookAdmin" visible="false" href="BookDetails.aspx?BookId=<%#:Item.BookId%>">
                                        <b> Редактировать книгу<b>
                                    </a>
                                    
                                  </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        </p>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table style="width:100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </section>
</asp:Content>
