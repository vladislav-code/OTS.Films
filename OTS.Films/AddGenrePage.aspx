<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGenrePage.aspx.cs" Inherits="OTS.Films.AddGenrePage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Форма для добавления жанра -->

        <h3>Добавить новый жанр</h3>
        <asp:Label ID="lblGenreNamee" runat="server" Text="Название жанра:" CssClass="form-label" />
        <asp:TextBox ID="txtGenreName" runat="server" CssClass="form-control" />
        <asp:Button ID="btnSaveGenre" runat="server" Text="Отправить" OnClick="btnSave_Click" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGenreName" 
            ErrorMessage="Имя должно быть указано" CssClass="error" Text="Название должно быть указано" />
        <br />
    </div>
</asp:Content>
