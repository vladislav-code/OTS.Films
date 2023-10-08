<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFilmPage.aspx.cs" Inherits="OTS.Films.AddFilmPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 209px">
        <!-- Форма для добавления фильма -->

        <h3>Добавить новый фильм</h3>
        <asp:Label ID="lblFilmTitle" runat="server" Text="Название фильма:" CssClass="form-label" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFilmTitle" 
                ErrorMessage="Имя должно быть указано" CssClass="error" Text="Название должно быть указано" />
        <asp:TextBox ID="txtFilmTitle" runat="server" CssClass="form-control" Width="214px" />
        <asp:Label ID="Label1" runat="server" Text="Режиссер фильма:" CssClass="form-label" />

        <asp:Label ID="Label2" runat="server" Text="Жанр фильма:" CssClass="form-label" />
        <br />

        <asp:ListBox id="lbDirectors" runat="server" SelectionMode="Multiple" DataValueField="id" DataTextField="name">
        </asp:ListBox>

        <asp:ListBox id="lbGenres" runat="server" SelectionMode="Multiple" DataValueField="id" DataTextField="name">
        </asp:ListBox>

        <asp:Button ID="btnSaveFilm" runat="server" Text="Отправить" OnClick="btnSave_Click" />

    </div>
</asp:Content>
